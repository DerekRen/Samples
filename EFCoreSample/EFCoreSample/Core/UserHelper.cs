using EFCoreSample.Models;
using Senparc.Weixin.QY.AdvancedAPIs;
using Senparc.Weixin.QY.AdvancedAPIs.MailList;
using Senparc.Weixin.QY.Containers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreSample.Core
{
    /// <summary>
    /// 用户帮助类，用于将用户组，Tag等信息转换到具体的用户
    /// 该类可能在通知中用到
    /// </summary>
    public class UserHelper
    {
        private static string corpID = "";
        private static string corpSecret = "";

        /// <summary>
        /// Get the wechat users and save to database.
        /// </summary>
        /// <returns></returns>
        public static bool GetWechatUserToDB()
        {
            string token = AccessTokenContainer.TryGetToken(corpID, corpSecret);
            WechatContext context = new WechatContext();

            //todo: 清空现有用户数据

            //获取微信企业号内的用户架构信息
            Dictionary<string, List<Tag>> _userTags = new Dictionary<string, List<Tag>>();
            Dictionary<int, List<Tag>> _partyTags = new Dictionary<int, List<Tag>>();

            Dictionary<string, User> Users = new Dictionary<string, User>();

            //查找所有Tag并插入数据库
            GetTagListResult tagList = MailListApi.GetTagList(token);

            if (tagList != null && tagList.taglist != null && tagList.taglist.Count > 0)
            {
                foreach (var tag in tagList.taglist)
                {
                    int tagId = -1;
                    if (Int32.TryParse(tag.tagid, out tagId))
                    {
                        Tag tempTag = new Tag() { TagId = tagId, TagName = tag.tagname };

                        GetTagMemberResult tagMemberResult = MailListApi.GetTagMember(token, tagId);
                        if (tagMemberResult != null && tagMemberResult.partylist != null && tagMemberResult.partylist.Length > 0)
                        {
                            foreach (int party in tagMemberResult.partylist)
                            {
                                if (!_partyTags.ContainsKey(party))
                                    _partyTags[party] = new List<Tag>();

                                _partyTags[party].Add(tempTag);
                            }
                        }

                        if (tagMemberResult != null && tagMemberResult.userlist != null && tagMemberResult.userlist.Count > 0)
                        {
                            foreach (var tagUser in tagMemberResult.userlist)
                            {
                                if (!_userTags.ContainsKey(tagUser.userid))
                                    _userTags[tagUser.userid] = new List<Tag>();

                                _userTags[tagUser.userid].Add(tempTag);
                            }
                        }

						context.Tags.Add(tempTag);
                    }
                }
                context.SaveChanges();
            }

            //查找所有部门并插入数据库
            GetDepartmentListResult departmentList = MailListApi.GetDepartmentList(token);

            if (departmentList != null && departmentList.department != null)
            {
                foreach (var party in departmentList.department)
                {
                    var tempParty = new Party() { PartyId = party.id, Name = party.name, Order = party.order, ParentPartyId = party.parentid };

                    //此处需要查询所有的Tag保存到库中
                    if (_partyTags.ContainsKey(party.id))
                    {
						tempParty.PartyTags = _partyTags[party.id].Select(f => new PartyTag() { PartyId = tempParty.PartyId, TagId = f.TagId }).ToList();
                    }

                    //根据部门查找所有用户并存入缓存
                    GetDepartmentMemberInfoResult memberInfos = MailListApi.GetDepartmentMemberInfo(token, party.id, 1, 0);
                    if (memberInfos != null && memberInfos.userlist != null && memberInfos.userlist.Count > 0)
                    {
                        foreach (var member in memberInfos.userlist)
                        {
                            if (!Users.ContainsKey(member.userid))
                            {
                                Users[member.userid] = new User()
                                {
                                    Avatar = member.avatar,
                                    Email = member.email,
                                    Gender = member.gender,
                                    Mobile = member.mobile,
                                    Name = member.name,
                                    Position = member.position,
                                    Status = member.status,
                                    UserId = member.userid,
                                    Weixinid = member.weixinid,
									UserTags = (_userTags.ContainsKey(member.userid)&& _userTags[member.userid].Count > 0) ? _userTags[member.userid].Select(f => new UserTag() { UserId = member.userid, TagId = f.TagId }).ToList() : null                                   
                                };
                            }
							Users[member.userid].UserPartys.Add(new UserParty() { PartyId = tempParty.PartyId, UserId = member.userid });
                        }
                    }

                    context.Partys.Add(tempParty);
                }
                context.SaveChanges();
            }

            if (Users != null && Users.Count > 0)
            {
                foreach(var user in  Users.Values)
					context.Users.Add(user);

                context.SaveChanges();
            }

            return true;
        }
    }
}
