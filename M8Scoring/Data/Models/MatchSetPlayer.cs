using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class MatchSetPlayer {

		#region Constructor
		public MatchSetPlayer() {

		}

		public MatchSetPlayer(MatchSet matchSet) {
			this.MatchSet = MatchSet;
		}
		#endregion

		#region Properties
		[Key]
		[Required]
		public int Id { get; set; }
		public int Number { get; set; }
		public int? PlayerId { get; set; }
		public virtual Player Player { get; set; }
		public int Rate { get; set; }
		public int Score { get; set; }
		public bool Forfeit { get; set; }

		public int MatchSetId { get; set; }
		public virtual MatchSet MatchSet { get; set; }

		#endregion
	}
}
