using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace M8Scoring.Data {
	public class ApplicationDbContext : DbContext {

		#region "Constructors"
		public ApplicationDbContext(DbContextOptions options) : base(options) {	}
		#endregion

		#region "Methods"
		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			modelBuilder.Entity<TeamPlayer>()
				.HasKey(tp => new { tp.TeamId, tp.PlayerId });

			modelBuilder.Entity<TeamPlayer>()
				.HasOne(tp => tp.Team).WithMany(p => p.TeamPlayers)
				.HasForeignKey(tp => tp.TeamId);

			modelBuilder.Entity<TeamPlayer>()
				.HasOne(tp => tp.Player).WithMany(t => t.TeamPlayers)
				.HasForeignKey(tp => tp.PlayerId);

		}
		#endregion

		#region "Properties"
		public DbSet<ApplicationUser> Users { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<Player> Players { get; set; }
		#endregion

	}
}
