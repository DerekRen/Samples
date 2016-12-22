using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreSample.Models
{
	public class UserTag
	{
		[StringLength(32)]
		public string UserId { set; get; }
		public User User { set; get; }

		public int TagId { set; get; }
		public Tag Tag { set; get; }
	}
}
