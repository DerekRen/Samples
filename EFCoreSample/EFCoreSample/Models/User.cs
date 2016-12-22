using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSample.Models
{
    public class User
    {
        public User()
        {
            UserPartys = new List<UserParty>();
            UserTags = new List<UserTag>();
        }

        /// <summary>
        /// 员工UserID
        /// </summary>
        [Key]
        [StringLength(32)]
        public string UserId { get; set; }
        /// <summary>
        /// 头像url。注：小图将url最后的"/0"改成"/64"
        /// </summary>
        [StringLength(256)]
        public string Avatar { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [StringLength(256)]
        public string Email { get; set; }
        /// <summary>
        /// 性别。gender=0表示男，=1表示女
        /// </summary>
        public int Gender { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [StringLength(32)]
        public string Mobile { get; set; }
        /// <summary>
        /// 成员名称
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// 职位信息
        /// </summary>
        [StringLength(64)]
        public string Position { get; set; }
        /// <summary>
        /// 关注状态: 1=已关注，2=已冻结，4=未关注
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [StringLength(64)]
        public string Weixinid { get; set; }
        /// <summary>
        /// 成员所属部门列表
        /// </summary>
		public List<UserParty> UserPartys { get; set; }
		public List<UserTag> UserTags { set; get; }
    }
}
