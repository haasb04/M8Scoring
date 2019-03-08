using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace M8Scoring.Controllers {
	[Route("api/[controller]")]
	public class AdminController : BaseApiController {
		public AdminController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration) : base(context, roleManager, userManager, configuration) {
		}

		[HttpPost("UploadM8Data"), DisableRequestSizeLimit]
		public IActionResult UploadM8Data() {
			try {
				var file = Request.Form.Files[0];
				if(file.Length > 0) {
					//parse the file
				}
			} catch(Exception) { }

			return new OkResult();
		}

		//// GET: api/<controller>
		//[HttpGet]
		//      public IEnumerable<string> Get()
		//      {
		//          return new string[] { "value1", "value2" };
		//      }

		//      // GET api/<controller>/5
		//      [HttpGet("{id}")]
		//      public string Get(int id)
		//      {
		//          return "value";
		//      }

		//      // POST api/<controller>
		//      [HttpPost]
		//      public void Post([FromBody]string value)
		//      {
		//      }

		//      // PUT api/<controller>/5
		//      [HttpPut("{id}")]
		//      public void Put(int id, [FromBody]string value)
		//      {
		//      }

		//      // DELETE api/<controller>/5
		//      [HttpDelete("{id}")]
		//      public void Delete(int id)
		//      {
		//      }
	}
}
