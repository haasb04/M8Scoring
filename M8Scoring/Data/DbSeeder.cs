using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class DbSeeder {
		#region "Public Methods"
		public static void Seed(ApplicationDbContext dbContext) {
			//create default users (if there are none)
			if(!dbContext.Users.Any()) {
				createUsers(dbContext);
			}

			//create test players and teams
			if(!dbContext.Teams.Any()) {
				createTestTeams(dbContext);
			}
		}
		#endregion

		#region "Seed Methods"
		private static void createUsers(ApplicationDbContext dbContext) {

			DateTime createdDate = new DateTime(2019, 01, 10, 12, 30, 0);
			DateTime lastModifiedDate = DateTime.Now;

			//create admin user
			var user_Admin = new ApplicationUser() { Id = Guid.NewGuid().ToString(), UserName = "Admin", Email = "admin@m8score.com", CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
			dbContext.Users.Add(user_Admin);

#if(DEBUG)
			//create some samples
			var user_Ryan = new ApplicationUser() { Id = Guid.NewGuid().ToString(), UserName = "Ryan", Email = "ryan@m8score.com", CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
			var user_Solice = new ApplicationUser() { Id = Guid.NewGuid().ToString(), UserName = "Solice", Email = "solice@m8score.com", CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
			var user_Vodan = new ApplicationUser() { Id = Guid.NewGuid().ToString(), UserName = "Vodan", Email = "vodan@m8score.com", CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };

			dbContext.Users.AddRange(user_Ryan, user_Solice, user_Vodan);
#endif

			dbContext.SaveChanges();
		}

		private static void createTestTeams(ApplicationDbContext dbContext) {
			DateTime createdDate = new DateTime(2019, 01, 10, 12, 30, 0);
			DateTime lastModifiedDate = DateTime.Now;

			for(int i = 1; i <= 100; i++) {
				var p = new Player() { Number = i, FirstName = string.Format("P{0}", i), LastName = string.Format("LN{0}", i), Rating = 65, CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
				dbContext.Players.Add(p);
			}
			dbContext.SaveChanges();

			for(int i = 0; i < 50; i++) {
				//create team
				var t = new Team() { Number = i, Name = string.Format("Team {0}", i+1), CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
				dbContext.Teams.Add(t);
				//add players
				int playerStart = i * 5;
				for(int o = 0; o < 5; o++) {
					int playerNum = playerStart + o + 1;
					var player = dbContext.Players.Where(p => p.Number == playerNum).SingleOrDefault();
					t.TeamPlayers.Add(new TeamPlayer() { Team = t, Player = player });					
				}
			}
			dbContext.SaveChanges();
		}

		#endregion
	}
}
