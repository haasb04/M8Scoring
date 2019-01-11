using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class Team {

		public Team() {

		}

		public ICollection<TeamPlayer> TeamPlayers { get; } = new List<TeamPlayer>();

		#region "Methods"

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
