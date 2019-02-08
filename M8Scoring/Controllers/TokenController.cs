using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using M8Scoring.ViewModels;
using M8Scoring.Data;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace M8Scoring.Controllers {
	[Route("api/[controller]")]
	public class TokenController : BaseApiController {
		#region Private Members
		#endregion Private Members

		#region Constructor
		public TokenController(
				ApplicationDbContext context,
				RoleManager<IdentityRole> roleManager,
				UserManager<ApplicationUser> userManager,
				IConfiguration configuration
				)
				: base(context, roleManager, userManager, configuration) { }
		#endregion

		[HttpPost("Auth")]
		public async Task<IActionResult> Auth([FromBody]TokenRequestViewModel model) {
			// return a generic HTTP Status 500 (Server Error)
			// if the client payload is invalid.
			if(model == null)
				return new StatusCodeResult(500);

			switch(model.grant_type) {
				case "password":
					return await GetToken(model);
				case "refresh_token":
					return await RefreshToken(model);
				default:
					// not supported - return a HTTP 401 (Unauthorized)
					return new UnauthorizedResult();
			}
		}

		private async Task<IActionResult> RefreshToken(TokenRequestViewModel model) {
			try {
				//check if recieved refresh token exists for the given clientid
				var rt = DbContext.Tokens.FirstOrDefault(t => t.ClientId == model.client_id && t.Value == model.refresh_token);

				if(rt == null) {
					return Json(null); // UnauthorizedResult();
				}

				//generate a nw refresh token
				var rtNew = createRefreshToken(rt.ClientId, rt.UserId);

				//invalidate the old refresh token
				DbContext.Tokens.Remove(rt);

				//add the new refresh token
				DbContext.Tokens.Add(rtNew);

				DbContext.SaveChanges();

				var response = createAccessToken(rtNew.UserId, rtNew.Value);
				return Json(response);
			} catch(Exception ex) {
				//TODO: LOG THIS
				return new StatusCodeResult(500); // UnauthorizedResult();
			}
		}

		private Token createRefreshToken(string clientId, string userId) {
			return new Token()
			{
				ClientId = clientId,
				UserId = userId,
				Type = 0,
				Value = Guid.NewGuid().ToString("N"),
				CreatedDate = DateTime.UtcNow
			};
		}

		private TokenResponseViewModel createAccessToken(string userId, string refreshToken) {
			DateTime now = DateTime.UtcNow;
			//add the regisered claim for JWT (RFC7519)
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, userId),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
			};

			var tokenExpirationMins = Configuration.GetValue<int>("Auth:Jwt:TokenExpirationInMinutes");
			var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:Jwt:Key"]));

			var token = new JwtSecurityToken(
				issuer: Configuration["Auth:Jwt:Issuer"],
				audience: Configuration["Auth:Jwt:Audience"],
				claims: claims,
				notBefore: now,
				expires: now.Add(TimeSpan.FromMinutes(tokenExpirationMins)),
				signingCredentials: new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256)
				);

			var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

			return new TokenResponseViewModel()
			{
				token = encodedToken,
				expiration = tokenExpirationMins,
				refresh_token = refreshToken,
				roles = createRolesArray(DbContext.Users.FirstOrDefault(u => u.Id == userId))
			};
		}

		private string[] createRolesArray(ApplicationUser user) {
			//roles
			IList<string> rolesList = UserManager.GetRolesAsync(user).Result;
			string[] rolesArray = new string[rolesList.Count];
			rolesList.CopyTo(rolesArray, 0);

			return rolesArray;
		}

		private async Task<IActionResult> GetToken(TokenRequestViewModel model) {
			try {
				// check if there's an user with the given username
				var user = await UserManager.FindByNameAsync(model.username);
				// fallback to support e-mail address instead of username
				if(user == null && model.username.Contains("@"))
					user = await UserManager.FindByEmailAsync(model.username);

				if(user == null
						|| !await UserManager.CheckPasswordAsync(user, model.password)) {
					// user does not exists or password mismatch
					return new UnauthorizedResult();
				}

				// username & password matches: create and return the Jwt token.
				var rt = createRefreshToken(model.client_id, user.Id);

				DbContext.Tokens.Add(rt);
				DbContext.SaveChanges();

				var t = createAccessToken(user.Id, rt.Value);
				return Json(t);
			} catch(Exception ex) {
				return new UnauthorizedResult();
			}
		}
	}
}
