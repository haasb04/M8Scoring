using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace M8Scoring.Data {
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {

		#region "Constructors"
		public ApplicationDbContext(DbContextOptions options) : base(options) {	}
		#endregion

		#region "Methods"
		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<TeamPlayer>()
				.HasKey(tp => new { tp.TeamId, tp.PlayerId });

			modelBuilder.Entity<TeamPlayer>()
				.HasOne(tp => tp.Team).WithMany(p => p.TeamPlayers)
				.HasForeignKey(tp => tp.TeamId);

			modelBuilder.Entity<TeamPlayer>()
				.HasOne(tp => tp.Player).WithMany(t => t.TeamPlayers)
				.HasForeignKey(tp => tp.PlayerId);

			modelBuilder.Entity<SubscriptionTeam>()
				.HasKey(st => new { st.SubscriptionId, st.TeamId });

			modelBuilder.Entity<SubscriptionTeam>()
				.HasOne(st => st.Subscription).WithMany(t => t.SubscriptionTeams)
				.HasForeignKey(st => st.SubscriptionId);
		}
		#endregion

		#region "Properties"
		//public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Player> Players { get; set; }
		public DbSet<Subscription> Subscriptions { get; set; }
		
		#endregion

	}
}
