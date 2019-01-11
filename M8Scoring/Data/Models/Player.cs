using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class Player {

		public Player() {

		}

		public ICollection<TeamPlayer> TeamPlayers { get; } = new List<TeamPlayer>();

		#region "Properties"
		[Key]
		[Required]
		public int Id { get; set; }

		[Required]
		public int Number { get; set; }

		[Required]
		public string FirstName { get; set; }

		[Required]
		public string LastName { get; set; }

		[Required]
		public int Rating { get; set; }

		[Required]
		public DateTime CreatedDate { get; set; }

		[Required]
		public DateTime LastModifiedDate { get; set; }
		#endregion

	}
}
