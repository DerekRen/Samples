using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSample.Models
{
    public class Party
    {
        public Party()
        {
            UserPartys = new List<UserParty>();
            PartyTags = new List<PartyTag>();
        }

        /// <summary>
        /// 部门Id
        /// </summary>
        [Key]
        public int PartyId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        /// <summary>
        /// 在父部门中的次序值。order值小的排序靠前。
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 上级部门Id
        /// </summary>
        public int ParentPartyId { get; set; }
        public List<UserParty> UserPartys { set; get; }
		public List<PartyTag> PartyTags { set; get; }
    }
}
