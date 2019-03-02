using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	public class MatchSummaryViewModel {
		public MatchSummaryViewModel() {

		}

		public int Id { get; set; }
		public string Date { get; set; }
		public string Opponent { get; set; }
		public string Score { get; set; }
	}
}
