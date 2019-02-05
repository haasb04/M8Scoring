using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;
using M8Scoring.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace M8Scoring.Controllers {
	[Route("api/[controller]")]
	public class PlayerController : BaseApiController {
		#region Private Fields

		#endregion

		#region Constructors
		public PlayerController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration):base(context, roleManager, userManager, configuration){
			
		}
		#endregion

		#region QuickSearch
		[HttpGet("QS/{term}")]
		public IActionResult QS(string term) {
			int termAsInt = 0;
			int.TryParse(term, out termAsInt);

			var list = DbContext.Players
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
			viewModel.PrepareData(DbContext.Players);
			//viewModel
			//{ "PageSize":x, "TotalCount":x, "TotalPages":x, "PageIndex":x, "HasPreviousPage":true, "HasNextPage":false, "object[]":Data}
			return new JsonResult(viewModel, new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		#region RESTful convention methods
		[HttpGet("{id}")]
		public IActionResult Get(int id) {

			var player = DbContext.Players
								.Include(p => p.TeamPlayers)
								.ThenInclude(tp => tp.Team)
								.Where(i => i.Id == id).FirstOrDefault();

			if(player == null) {
				return NotFound(new { Error = string.Format("Player {0} was not found.", id) });
			}

			PlayerViewModel vm = player.Adapt<PlayerViewModel>();

			//teams
			List<TeamViewModel> teams = new List<TeamViewModel>();
			foreach(TeamPlayer tp in player.TeamPlayers) {
				teams.Add(tp.Team.Adapt<TeamViewModel>());
			}

			vm.Teams = teams.ToArray();

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
			DbContext.Players.Add(player);
			DbContext.SaveChanges();

			return new JsonResult(player.Adapt<PlayerViewModel>(), new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}


		[HttpPost]
		public IActionResult Post([FromBody]PlayerViewModel model) {
			if(model == null) {
				return new StatusCodeResult(500);
			}

			var player = DbContext.Players.Where(t => t.Id == model.Id).FirstOrDefault();

			if(player == null) {
				return NotFound(new { Error = string.Format("player ID {0} was not found.", model.Id) });
			}

			//hand the update
			player.FirstName = model.FirstName;
			player.LastName = model.LastName;
			player.Number = model.Number;
			player.Rating = model.Rating;

			player.LastModifiedDate = DateTime.Now;
			DbContext.SaveChanges();
			return new JsonResult(player.Adapt<PlayerViewModel>(), new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id) {
			var player = DbContext.Players.Where(t => t.Id == id).FirstOrDefault();

			if(player == null) {
				return NotFound(new { Error = string.Format("Player {0} was not found.", id) });
			}

			DbContext.Players.Remove(player);  //cascase delete????
			DbContext.SaveChanges();

			return new OkResult();
		}
		#endregion
	}
}