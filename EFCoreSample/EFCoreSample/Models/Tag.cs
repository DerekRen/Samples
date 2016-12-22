using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreSample.Models
{
    public class Tag
    {
        public Tag()
        {
            PartyTags = new List<PartyTag>();
            UserTags = new List<UserTag>();
        }

        [Key]
        public int TagId { set; get; }
        [Required]
        [StringLength(64)]
        public string TagName { set; get; }
        public List<PartyTag> PartyTags { get; set; }
        public List<UserTag> UserTags { get; set; }
    }
}
