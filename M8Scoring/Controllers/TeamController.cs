using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;
using M8Scoring.ViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
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

		// GET: api/<controller>
		[HttpGet("all/{num}")]
		public IActionResult All(int num = 10) {
			var all = mDbContext.Teams
			 .OrderByDescending(t => t.CreatedDate)
			 .Take(num)
			 .ToArray();

			return new JsonResult(all.Adapt<TeamViewModel[]>(), new JsonSerializerSettings() { Formatting = Formatting.Indented });
		}

		//// GET api/<controller>/5
		//[HttpGet("{id}")]
		//public string Get(int id) {
		//	return "value";
		//}

		//// POST api/<controller>
		//[HttpPost]
		//public void Post([FromBody]string value) {
		//}

		//// PUT api/<controller>/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody]string value) {
		//}

		//// DELETE api/<controller>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id) {
		//}
	}
}
