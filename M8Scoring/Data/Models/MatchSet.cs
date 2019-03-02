using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class MatchSet {
		#region Constructor
		public MatchSet() {

		}

		public MatchSet(Match match) {
			this.Match = match;
		}
		#endregion

		#region Properties
		[Key]
		[Required]
		public int Id { get; set; }
		public int SetNumber { get; set; }

		public List<MatchSetPlayer> Players { get; set; } = new List<MatchSetPlayer>();

		public MatchSetPlayer Player1 {
			get {
				return Players.Where(s => s.Number == 1).FirstOrDefault();
			}
		}

		public MatchSetPlayer Player2 { get {
				return Players.Where(s => s.Number == 2).FirstOrDefault();
			}
		}

		public bool Win { get; set; }

		public int MatchId { get; set; }
		public virtual Match Match { get; set; }
		#endregion
	}
}
