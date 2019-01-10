using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class Team {

		public Team() {
			this.TeamPlayers = new HashSet<TeamPlayer>();
		}

		public ICollection<TeamPlayer> TeamPlayers { get; set; }

		#region "Methods"

		#endregion

		#region "Properties"
		[Key]
		[Required]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
