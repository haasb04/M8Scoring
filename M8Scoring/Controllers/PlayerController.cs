using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;
using M8Scoring.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace M8Scoring.Controllers {
	[Route("api/[controller]")]
	public class PlayerController : Controller {
		#region Private Fields
		private ApplicationDbContext mDbContext;
		#endregion

		#region Constructors
		public PlayerController(ApplicationDbContext context) {
			mDbContext = context;
		}
		#endregion

		#region QuickSearch
		[HttpGet("QS/{term}")]
		public IActionResult QS(string term) {
			int termAsInt = 0;
			int.TryParse(term, out termAsInt);

			var list = mDbContext.Players
				.Where(t => t.FirstName.Contains(term) || t.LastName.Contains(term) || t.Number == termAsInt)
				.Take(10)
				.Select(t => t.LastName);

			return new JsonResult(list, new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		#endregion



		[HttpGet("all")]
		public IActionResult GetPlayers(string listSpfInput) {
			if(string.IsNullOrEmpty(listSpfInput)) {
				return StatusCode(500, new BadRequestObjectResult("No Inputs"));
			}
			PlayerListViewModel viewModel = new PlayerListViewModel(listSpfInput);
			viewModel.PrepareData(mDbContext.Players);
			//viewModel
			//{ "PageSize":x, "TotalCount":x, "TotalPages":x, "PageIndex":x, "HasPreviousPage":true, "HasNextPage":false, "object[]":Data}
			return new JsonResult(viewModel, new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		#region RESTful convention methods
		[HttpGet("{id}")]
		public IActionResult Get(int id) {

			var player = mDbContext.Players
								.Where(i => i.Id == id).FirstOrDefault();

			if(player == null) {
				return NotFound(new { Error = string.Format("Player {0} was not found.", id) });
			}

			PlayerViewModel vm = player.Adapt<PlayerViewModel>();

			////teams
			//List<TeamViewModel> teams = new List<TeamViewModel>();
			//foreach(TeamPlayer tp in player.TeamPlayers) {
			//	teams.Add(tp.Team.Adapt<TeamViewModel>());
			//}

			//vm.Teams = teams.ToArray();

			return new JsonResult(vm, new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		[HttpPut]
		public IActionResult Put([FromBody]PlayerViewModel model) {
			if(model == null) {
				return new StatusCodeResult(500);
			}

			var player = new Player();

			//properties taken from the request
			player.Number = model.Number;
			player.FirstName = model.FirstName;
			player.LastName = model.LastName;
			player.CreatedDate = DateTime.Now;
			player.LastModifiedDate = player.CreatedDate;

			//player owner???
			mDbContext.Players.Add(player);
			mDbContext.SaveChanges();

			return new JsonResult(player.Adapt<PlayerViewModel>(), new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}


		[HttpPost]
		public IActionResult Post([FromBody]PlayerViewModel model) {
			if(model == null) {
				return new StatusCodeResult(500);
			}

			var player = mDbContext.Players.Where(t => t.Id == model.Id).FirstOrDefault();

			if(player == null) {
				return NotFound(new { Error = string.Format("player ID {0} was not found.", model.Id) });
			}

			//hand the update
			player.FirstName = model.FirstName;
			player.LastName = model.LastName;
			player.Number = model.Number;

			player.LastModifiedDate = DateTime.Now;
			mDbContext.SaveChanges();

			return new JsonResult(player.Adapt<PlayerViewModel>(), new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id) {
			var player = mDbContext.Players.Where(t => t.Id == id).FirstOrDefault();

			if(player == null) {
				return NotFound(new { Error = string.Format("Player {0} was not found.", id) });
			}

			mDbContext.Players.Remove(player);  //cascase delete????
			mDbContext.SaveChanges();

			return new OkResult();
		}
		#endregion
	}
}