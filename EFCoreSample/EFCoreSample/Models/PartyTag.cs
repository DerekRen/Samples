using System;

namespace EFCoreSample.Models
{
	public class PartyTag
	{
		public int PartyId { set; get; }
		public Party Party { set; get; }

		public int TagId { set; get; }
		public Tag Tag { set; get; }
	}
}
