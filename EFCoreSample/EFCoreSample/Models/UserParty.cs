using System;
using System.ComponentModel.DataAnnotations;

namespace EFCoreSample.Models
{
	public class UserParty
	{
		[StringLength(32)]
		public string UserId { set; get; }
		public User User { set; get; }

		public int PartyId { set; get; }
		public Party Party { set; get; }
	}
}
