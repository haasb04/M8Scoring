using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using M8Scoring.Data;
using M8Scoring.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace M8Scoring.Controllers {
	[Authorize]
	[Route("api/[controller]")]
	public class HomeController : BaseApiController {
		public HomeController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(context, roleManager, userManager, configuration) {
		}

		[HttpGet("all")]
		public IActionResult All(int num = 10) {
			Subscription sub = DbContext.Subscriptions
				.Include(t => t.SubscriptionTeams)
				.Where(s => s.UserId == CurrentUser().Id).FirstOrDefault();

			List<TeamViewModel> vm = new List<TeamViewModel>();

			foreach(SubscriptionTeam st in sub.SubscriptionTeams) {
				Team t = DbContext.Teams.Find(st.TeamId);
				if(t != null) {
					vm.Add(t.Adapt<TeamViewModel>()); //intentionally omitting players
				}
			}

			return new JsonResult(vm.ToArray(), JsonSettings);
		}

		[HttpGet("QS/{teamId}/{term}")]
		public IActionResult QS(int teamId, string term) {
			int termAsInt = 0;
			int.TryParse(term, out termAsInt);

			var list = DbContext.Matches.Include(t => t.Team).Include(t => t.Opponent)
				.Where(m => m.Opponent.Name.Contains(term) || m.Id == termAsInt || m.Location.Contains(term))
				.Take(10)
				.Select(m => m.Opponent.Name);

			return new JsonResult(list, JsonSettings);

		}

		[HttpGet("matches/{teamId}/")]
		public IActionResult Matches(int teamId, string listSpfInput) {
			if(string.IsNullOrEmpty(listSpfInput)) {
				return StatusCode(500, new BadRequestObjectResult("No Inputs"));
			}

			MatchSummaryListViewModel viewModel = new MatchSummaryListViewModel(listSpfInput);
			viewModel.PrepareData(DbContext.Matches.Include(m => m.Opponent).Where(m => m.TeamId == teamId));

			return new JsonResult(viewModel, JsonSettings);
		}
	}
}