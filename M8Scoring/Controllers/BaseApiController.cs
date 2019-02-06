﻿using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using M8Scoring.Data;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using M8Scoring.Internal;

namespace M8Scoring.Controllers {
	[Route("api/[controller]")]
	public class BaseApiController : Controller {
		#region Constructor
		public BaseApiController(
				ApplicationDbContext context,
				RoleManager<IdentityRole> roleManager,
				UserManager<ApplicationUser> userManager,
				IConfiguration configuration
				) {
			// Instantiate the required classes through DI
			DbContext = context;
			RoleManager = roleManager;
			UserManager = userManager;
			Configuration = configuration;

			// Instantiate a single JsonSerializerSettings object
			// that can be reused multiple times.
			JsonSettings = new JsonSerializerSettings()
			{
				Formatting = Formatting.Indented
			};

		}
		#endregion

		#region Shared Properties
		protected ApplicationDbContext DbContext { get; private set; }
		protected RoleManager<IdentityRole> RoleManager { get; private set; }
		protected UserManager<ApplicationUser> UserManager { get; private set; }
		protected IConfiguration Configuration { get; private set; }
		protected JsonSerializerSettings JsonSettings { get; private set; }
		#endregion

		#region Authorization
		internal bool IsInRole(AuthRoles role) {
			try {
				string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
				return UserManager.IsInRoleAsync(DbContext.Users.Find(userId), role.Value).Result;
			} catch(Exception) {
				return false;
			}
		}

		#endregion
	}
}
