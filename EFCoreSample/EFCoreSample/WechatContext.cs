using EFCoreSample.Models;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

namespace EFCoreSample
{
    public class WechatContext : DbContext
    {
        public DbSet<User> Users { set; get; }
        public DbSet<Party> Partys { set; get; }
        public DbSet<Tag> Tags { set; get; }
		public DbSet<PartyTag> PartyTags { set; get; }
		public DbSet<UserParty> UserPartys { set; get; }
		public DbSet<UserTag> UserTags { set; get; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseMySQL(@"server=127.0.0.1;user id=root;password=root;persistsecurityinfo=True;database=wechatdb;Character Set=utf8");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<PartyTag>()
			            .HasKey(c => new { c.PartyId, c.TagId });
			modelBuilder.Entity<UserParty>()
			            .HasKey(c => new { c.PartyId, c.UserId });
			modelBuilder.Entity<UserTag>()
						.HasKey(c => new { c.UserId, c.TagId });

        }
    }
}
