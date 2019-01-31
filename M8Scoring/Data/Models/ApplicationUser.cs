﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace M8Scoring.Data {
	public class ApplicationUser : IdentityUser {
		public ApplicationUser() {

		}

		#region "Properties"

		//[Key]
		//[Required]
		//public string Id { get; set; }

		//[Required]
		//[MaxLength(128)]
		//public string UserName { get; set; }

		//[Required]
		//public string Email { get; set; }

		public string DisplayName { get; set; }

		public string Notes { get; set; }

		[Required]
		public int Type { get; set; }

		[Required]
		public int Flags { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }

		[Required]
		public DateTime LastModifiedDate { get; set; }

		#endregion
		
		#region Lazy-Load properties

		/// <summary>
		/// A list of all Teams for this user
		/// </summary>
		public virtual List<Team> Teams { get; set; }
		#endregion
	}
}
