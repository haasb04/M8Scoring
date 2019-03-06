using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using M8Scoring.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Authorization;

namespace M8Scoring.Controllers {
	[Authorize]
	[Route("api/[controller]")]
	public class MatchController : BaseApiController {

		#region Private Fields

		#endregion

		#region Constructors
		public MatchController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(context, roleManager, userManager, configuration) {
		}

		#endregion

		#region Restful methods

		// GET api/<controller>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id) {
			var match = DbContext.Matches
				.Include(t => t.Team)
				 .ThenInclude(p => p.TeamPlayers)
					.ThenInclude(tp => tp.Player)
				.Include(t => t.Opponent)
					.ThenInclude(o => o.TeamPlayers)
						.ThenInclude(tp => tp.Player)
				.Include(s => s.Sets)
					.ThenInclude(sp => sp.Players)
				.Where(i => i.Id == id).FirstOrDefault();

			if(match == null) {
				return StatusCode(500, new BadRequestObjectResult("Match doesnt exist"));
			}

			MatchViewModel vm = match.Adapt<MatchViewModel>();

			List<PlayerViewModel> teamPlayers = new List<PlayerViewModel>();
			foreach(TeamPlayer tp in match.Team.TeamPlayers) {
				teamPlayers.Add(tp.Player.Adapt<PlayerViewModel>());
			}
			vm.Team.Players = teamPlayers.ToArray();

			List<PlayerViewModel> opponentPlayers = new List<PlayerViewModel>();
			foreach(TeamPlayer tp in match.Opponent.TeamPlayers) {
				opponentPlayers.Add(tp.Player.Adapt<PlayerViewModel>());
			}
			vm.Opponent.Players = opponentPlayers.ToArray();


			return new JsonResult(vm, JsonSettings);
		}

		[HttpPost]
		public IActionResult Post([FromBody]MatchViewModel model) {
			var match = DbContext.Matches
				.Include(t => t.Team)
				 .ThenInclude(p => p.TeamPlayers)
					.ThenInclude(tp => tp.Player)
				.Include(t => t.Opponent)
					.ThenInclude(o => o.TeamPlayers)
						.ThenInclude(tp => tp.Player)
				.Include(s => s.Sets)
					.ThenInclude(sp => sp.Players)
				.Where(i => i.Id == model.Id).FirstOrDefault();

			if(match == null) {
				return StatusCode(500, new BadRequestObjectResult("Match doesnt exist"));
			}

			if(model.Set1.Player1.Player != null) {
				match.Set1.Player1.PlayerId = model.Set1.Player1.Player.Id;
				match.Set1.Player1.Rate = model.Set1.Player1.Rate;
				match.Set1.Player1.Score = model.Set1.Player1.Score;
			}

			if(model.Set1.Player2.Player != null) {
				match.Set1.Player2.PlayerId = model.Set1.Player2.Player.Id;
				match.Set1.Player2.Rate = model.Set1.Player2.Rate;
				match.Set1.Player2.Score = model.Set1.Player2.Score;
			}

			match.Set1.Win = model.Set1.Win;

			if(model.Set2.Player1.Player != null) {
				match.Set2.Player1.PlayerId = model.Set2.Player1.Player.Id;
				match.Set2.Player1.Rate = model.Set2.Player1.Rate;
				match.Set2.Player1.Score = model.Set2.Player1.Score;
			}

			if(model.Set2.Player2.Player != null) {
				match.Set2.Player2.PlayerId = model.Set2.Player2.Player.Id;
				match.Set2.Player2.Rate = model.Set2.Player2.Rate;
				match.Set2.Player2.Score = model.Set2.Player2.Score;
			}

			match.Set2.Win = model.Set2.Win;

			if(model.Set3.Player1.Player != null) {
				match.Set3.Player1.PlayerId = model.Set3.Player1.Player.Id;
				match.Set3.Player1.Rate = model.Set3.Player1.Rate;
				match.Set3.Player1.Score = model.Set3.Player1.Score;
			}

			if(model.Set3.Player2.Player != null) {
				match.Set3.Player2.PlayerId = model.Set3.Player2.Player.Id;
				match.Set3.Player2.Rate = model.Set3.Player2.Rate;
				match.Set3.Player2.Score = model.Set3.Player2.Score;
			}
			match.Set3.Win = model.Set3.Win;

			if(model.Set4.Player1.Player != null) {
				match.Set4.Player1.PlayerId = model.Set4.Player1.Player.Id;
				match.Set4.Player1.Rate = model.Set4.Player1.Rate;
				match.Set4.Player1.Score = model.Set4.Player1.Score;
			}

			if(model.Set4.Player2.Player != null) {
				match.Set4.Player2.PlayerId = model.Set4.Player2.Player.Id;
				match.Set4.Player2.Rate = model.Set4.Player2.Rate;
				match.Set4.Player2.Score = model.Set4.Player2.Score;
			}
			match.Set4.Win = model.Set4.Win;

			if(model.Set5.Player1.Player != null) {
				match.Set5.Player1.PlayerId = model.Set5.Player1.Player.Id;
				match.Set5.Player1.Rate = model.Set5.Player1.Rate;
				match.Set5.Player1.Score = model.Set5.Player1.Score;
			}

			if(model.Set5.Player2.Player != null) {
				match.Set5.Player2.PlayerId = model.Set5.Player2.Player.Id;
				match.Set5.Player2.Rate = model.Set5.Player2.Rate;
				match.Set5.Player2.Score = model.Set5.Player2.Score;
			}

			match.Set5.Win = model.Set5.Win;

			match.TotalOpponentScore = model.TotalOpponentScore;
			match.TotalScore = model.TotalOpponentScore;

			//override serverside only properties
			match.LastModifiedDate = DateTime.Now;


			DbContext.SaveChanges();

			return new OkResult();
		}

		[HttpPut]
		public IActionResult Put([FromBody]MatchHeaderViewModel model) {
			if(model == null) {
				return new StatusCodeResult(500);
			}

			//create a new match from provided header information and save
			Match match;
			switch(model.Level.ToLower()) {
				case "open":
				case "advance":
				case "masters":
				default:
					match = Match.CreateAdvancedMatch(model.IsRegularSeason);
					break;
			}

			match.TeamId = model.TeamId;
			match.OpponentId = model.OpponentId;
			match.Date = model.Date;
			match.Location = model.Location;

			//override serverside only properties
			match.CreatedDate = DateTime.Now;
			match.LastModifiedDate = match.CreatedDate;

			DbContext.Matches.Add(match);
			DbContext.SaveChanges();
			model.Id = match.Id;

			return new JsonResult(model, JsonSettings);
		}

		#endregion
	}
}