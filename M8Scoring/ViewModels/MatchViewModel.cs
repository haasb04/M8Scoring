using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	public class MatchViewModel {

		public MatchViewModel() {

		}

		public int Id { get; set; }
		public int Number { get; set; }
		public string Date { get; set; }
		public string Location { get; set; }
		public int WinMultiplier { get; set; }
		public int OverUnderPenalty { get; set; }
		public int PenaltyMultiplier { get; set; }
		public int NonRatedPlayerRate { get; set; }
		public bool IsRegularSeason { get; set; }

		//calculated Totals
		public int TotalScore { get; set; }
		public int TotalOpponentScore { get; set; }
		public int TotalRate { get; set; }
		public int TotalOpponentRate { get; set; }
		public int TeamBonusOrPenalty { get; set; }
		public int OpponentBonusOrPenalty { get; set; }

		public virtual TeamViewModel Team { get; set; }
		public virtual TeamViewModel Opponent { get; set; }

		public virtual MatchSetViewModel Set1 { get; set; }
		public virtual MatchSetViewModel Set2 { get; set; }
		public virtual MatchSetViewModel Set3 { get; set; }
		public virtual MatchSetViewModel Set4 { get; set; }
		public virtual MatchSetViewModel Set5 { get; set; }

	
	}
}
