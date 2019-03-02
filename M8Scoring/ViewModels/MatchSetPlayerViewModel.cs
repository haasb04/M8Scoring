using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	public class MatchSetPlayerViewModel {

		public MatchSetPlayerViewModel() {

		}

		public int Id { get; set; }

		public virtual PlayerViewModel Player { get; set; }

		public int Rate { get; set; }
		public int Score { get; set; }
		public bool Forfeit { get; set; }
	}
}
