using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace M8Scoring.Data {
	public class DbSeeder {
		#region "Public Methods"
		public static void Seed(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) {
			//create default users (if there are none)
			if(!dbContext.Users.Any()) {
				createUsers(dbContext, roleManager, userManager).GetAwaiter().GetResult();
				
			}

			//create test players and teams
			if(!dbContext.Teams.Any()) {
				createTestTeams(dbContext);
				createSubscriptionTeams(dbContext);
			}
		}
		#endregion

		#region "Seed Methods"
		private static async Task createUsers(ApplicationDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager) {

			DateTime createdDate = new DateTime(2019, 01, 10, 12, 30, 0);
			DateTime lastModifiedDate = DateTime.Now;

			string role_Administrator = "Administrator";
			string role_RegisteredUser = "RegisteredUser";

			//create roles
			if(!await roleManager.RoleExistsAsync(role_Administrator)) {
				await roleManager.CreateAsync(new IdentityRole(role_Administrator));
			}

			if(!await roleManager.RoleExistsAsync(role_RegisteredUser)) {
				await roleManager.CreateAsync(new IdentityRole(role_RegisteredUser));
			}

			//create admin user
			var user_Admin = new ApplicationUser() { SecurityStamp = Guid.NewGuid().ToString(), UserName = "Admin", Email = "admin@m8score.com", CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
			if(await userManager.FindByNameAsync(user_Admin.UserName) == null) {
				await userManager.CreateAsync(user_Admin, "Pass4Admin");
				await userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
				await userManager.AddToRoleAsync(user_Admin, role_Administrator);
				//Remove lockout and E-Mail confirmation
				user_Admin.EmailConfirmed = true;
				user_Admin.LockoutEnabled = false;
			}
			
#if(DEBUG)
			//create some samples
			var user_Ryan = new ApplicationUser() { SecurityStamp = Guid.NewGuid().ToString(), UserName = "Ryan", Email = "ryan@m8score.com", CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
			var user_Solice = new ApplicationUser() { SecurityStamp = Guid.NewGuid().ToString(), UserName = "Solice", Email = "solice@m8score.com", CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
			var user_Vodan = new ApplicationUser() { SecurityStamp = Guid.NewGuid().ToString(), UserName = "Vodan", Email = "vodan@m8score.com", CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };

			if(await userManager.FindByNameAsync(user_Ryan.UserName) == null) {
				await userManager.CreateAsync(user_Ryan, "Pass4Ryan");
				await userManager.AddToRoleAsync(user_Ryan, role_RegisteredUser);
				//Remove lockout and E-Mail confirmation
				user_Ryan.EmailConfirmed = true;
				user_Ryan.LockoutEnabled = false;
			}

			if(await userManager.FindByNameAsync(user_Vodan.UserName) == null) {
				await userManager.CreateAsync(user_Vodan, "Pass4Vodan");
				await userManager.AddToRoleAsync(user_Vodan, role_RegisteredUser);
				//Remove lockout and E-Mail confirmation
				user_Vodan.EmailConfirmed = true;
				user_Vodan.LockoutEnabled = false;
			}

			if(await userManager.FindByNameAsync(user_Solice.UserName) == null) {
				await userManager.CreateAsync(user_Solice, "Pass4Solice");
				await userManager.AddToRoleAsync(user_Solice, role_RegisteredUser);
				//Remove lockout and E-Mail confirmation
				user_Solice.EmailConfirmed = true;
				user_Solice.LockoutEnabled = false;
			}

#endif

			await dbContext.SaveChangesAsync(); 
		}

		private static void createTestTeams(ApplicationDbContext dbContext) {
			DateTime createdDate = new DateTime(2019, 01, 10, 12, 30, 0);
			DateTime lastModifiedDate = DateTime.Now;

			using(var tr = dbContext.Database.BeginTransaction()) {
				try {
					for(int i = 0; i < 250; i++) {
						var p = new Player() { Number = i + 1, FirstName = string.Format("P{0}", i + 1), LastName = string.Format("LN{0}", i + 1), Rating = 65, CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
						dbContext.Players.Add(p);
						dbContext.SaveChanges();
					}

					for(int i = 0; i < 50; i++) {
						//create team
						var t = new Team() { Number = i + 1, Name = string.Format("Team {0}", i + 1), CreatedDate = createdDate, LastModifiedDate = lastModifiedDate };
						dbContext.Teams.Add(t);
						dbContext.SaveChanges();

						//add players
						int playerStart = i * 5;
						for(int o = 0; o < 5; o++) {
							int playerNum = playerStart + o + 1;
							var player = dbContext.Players.Where(p => p.Number == playerNum).SingleOrDefault();
							//if(player != null) {
							t.TeamPlayers.Add(new TeamPlayer() { Team = t, Player = player });
							//}
							if(player == null) {
								System.Diagnostics.Debug.WriteLine(playerNum);
							}
						}
					}
					dbContext.SaveChanges();
					tr.Commit();
				} catch(Exception) {
					tr.Rollback();
				}
			}
		}

		private static void createSubscriptionTeams(ApplicationDbContext dbContext) {
			DateTime createdDate = new DateTime(2019, 01, 10, 12, 30, 0);
			DateTime lastModifiedDate = DateTime.Now;

			using(var tr = dbContext.Database.BeginTransaction()) {
				try {
#if(DEBUG)
					//user_Ryan
					var ryan = dbContext.Users
						.Include(s => s.Subscription)
						.ThenInclude( t => t.SubscriptionTeams)
						.Where(u => u.UserName == "Ryan").FirstOrDefault();
			
					var team1 = dbContext.Teams.Find(1);
					var team2 = dbContext.Teams.Find(2);

					ryan.Subscription.SubscriptionTeams.Add(new SubscriptionTeam() { Team = team1, Subscription = ryan.Subscription });
					ryan.Subscription.SubscriptionTeams.Add(new SubscriptionTeam() { Team = team2, Subscription = ryan.Subscription });

					//user vodan
					var vodan = dbContext.Users
						.Include(s => s.Subscription)
						.ThenInclude(t => t.SubscriptionTeams)
						.	Where(u => u.UserName == "Vodan").FirstOrDefault();
					var team3 = dbContext.Teams.Find(3);

					vodan.Subscription.SubscriptionTeams.Add(new SubscriptionTeam() { Team = team3, Subscription = vodan.Subscription });
#endif
					dbContext.SaveChanges();
					tr.Commit();
				} catch(Exception) {
					tr.Rollback();
				}
			}

		}
		#endregion
	}
}
