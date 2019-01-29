using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;
using M8Scoring.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace M8Scoring.Controllers {
	[Route("api/[controller]")]
	public class TeamController : Controller {
		#region Private Fields
		private ApplicationDbContext mDbContext;
		#endregion

		#region Constructors
		public TeamController(ApplicationDbContext context) {
			mDbContext = context;
		}
		#endregion

		#region QuickSearch
		[HttpGet("QS/{term}")]
		public IActionResult QS(string term) {
			int termAsInt = 0;
			int.TryParse(term, out termAsInt);

			var list = mDbContext.Teams
				.Where(t => t.Name.Contains(term) || t.Number == termAsInt)
				.Take(10)
				.Select(t => t.Name);

			return new JsonResult(list, new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		#endregion

		// GET: api/<controller>
		[HttpGet("all/{num}")]
		public IActionResult All(int num = 10) {
			var all = mDbContext.Teams
			 .OrderByDescending(t => t.CreatedDate)
			 .Take(num)
			 .ToArray();

			return new JsonResult(all.Adapt<TeamViewModel[]>(), new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		[HttpGet("all")]
		public IActionResult GetTeams(string listSpfInput) {
			if(string.IsNullOrEmpty(listSpfInput)) {
				return StatusCode(500, new BadRequestObjectResult("No Inputs"));
			}
			TeamListViewModel viewModel = new TeamListViewModel(listSpfInput);
			viewModel.PrepareData(mDbContext.Teams);
			//viewModel
			//{ "PageSize":x, "TotalCount":x, "TotalPages":x, "PageIndex":x, "HasPreviousPage":true, "HasNextPage":false, "object[]":Data}
			return new JsonResult(viewModel, new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		#region RESTful convention methods

		// GET api/<controller>/5
		[HttpGet("{id}")]
		public IActionResult Get(int id) {

			var team = mDbContext.Teams
								.Include(t => t.TeamPlayers)
								 .ThenInclude(tp => tp.Player)
								.Where(i => i.Id == id).FirstOrDefault();

			if(team == null) {
				return NotFound(new { Error = string.Format("Team {0} was not found.", id) });
			}

			TeamViewModel vm = team.Adapt<TeamViewModel>();
			
			//players
			List<PlayerViewModel> players = new List<PlayerViewModel>();
			foreach(TeamPlayer tp in team.TeamPlayers) {
				players.Add(tp.Player.Adapt<PlayerViewModel>());
			}

			vm.Players = players.ToArray();

			return new JsonResult(vm, new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		/// <summary>
		/// Adds a new Team to the database
		/// </summary>
		/// <param name="model">The TeamViewModel containing the new team information.</param>
		/// <param name="value"></param>
		[HttpPut]
		public IActionResult Put([FromBody]TeamViewModel model) {
			if(model == null) {
				return new StatusCodeResult(500);
			}

			var team = new Team();

			//properties taken from the request
			team.Number = model.Number;
			team.Name = model.Name;
			team.CreatedDate = DateTime.Now;
			team.LastModifiedDate = team.CreatedDate;

			//team owner???
			mDbContext.Teams.Add(team);
			mDbContext.SaveChanges();

			return new JsonResult(team.Adapt<TeamViewModel>(), new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		/// <summary>
		/// Edit the team with the given id
		/// </summary>
		/// <param name="model">The TeamViewModel</param>
		[HttpPost]
		public IActionResult Post([FromBody]TeamViewModel model) {
			if(model == null) {
				return new StatusCodeResult(500);
			}

			var team = mDbContext.Teams
				.Include(t => t.TeamPlayers)
				.Where(t => t.Id == model.Id).FirstOrDefault();

			if(team == null) {
				return NotFound(new { Error = string.Format("Team ID {0} was not found.", model.Id) });
			}

			//handle the update
			team.Name = model.Name;
			team.Number = model.Number;

			team.LastModifiedDate = DateTime.Now;

			foreach(PlayerViewModel pvm in model.Players) {
				//look for added players
				TeamPlayer tp = team.TeamPlayers.Where(p => p.PlayerId == pvm.Id).FirstOrDefault();

				if(tp == null) {
					//add it
					TeamPlayer playerToAdd = new TeamPlayer();
					playerToAdd.TeamId = team.Id;
					playerToAdd.PlayerId = pvm.Id;
					team.TeamPlayers.Add(playerToAdd);
				}
			}

			List<TeamPlayer> playersToRemove = new List<TeamPlayer>();
			foreach(TeamPlayer p in team.TeamPlayers) {
				//look for player in pvm
				PlayerViewModel pvm = model.Players.Where(tp => tp.Id == p.PlayerId).FirstOrDefault();
				if(pvm == null) {
					//player was removed
					playersToRemove.Add(p);
				}
			}

			foreach(TeamPlayer p in playersToRemove) {
				team.TeamPlayers.Remove(p);
			}
			
			mDbContext.SaveChanges();

			return new JsonResult(team.Adapt<TeamViewModel>(), new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		/// <summary>
		/// Deletes the Team with the given id.
		/// </summary>
		/// <param name="id">The id of the team to delete.</param>
		[HttpDelete("{id}")]
		public IActionResult Delete(int id) {
			var team = mDbContext.Teams.Where(t => t.Id == id).FirstOrDefault();

			if(team == null) {
				return NotFound(new { Error = string.Format("Team {0} was not found.", id)});
			}

			mDbContext.Teams.Remove(team);
			mDbContext.SaveChanges();

			return new OkResult();
		}
		#endregion
	}
}
