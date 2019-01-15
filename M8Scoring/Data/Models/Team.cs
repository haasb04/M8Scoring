using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace M8Scoring.Data {
	public class Team {

		public Team() {

		}

		public ICollection<TeamPlayer> TeamPlayers { get; } = new List<TeamPlayer>();

		#region "Methods"
		public static PaginatedList<TeamViewModel> PrepareData(DbSet<Team> data, ListSpfOutput sortFilter) {
			return null;
		}
		#endregion

		#region "Properties"
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int Number { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }

		[Required]
		public DateTime LastModifiedDate { get; set; }

		#endregion
	}
}
