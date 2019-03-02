using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	public class MatchHeaderViewModel {
		public MatchHeaderViewModel() {

		}

		public int Id { get; set; }
		public int TeamId { get; set; }
		public int OpponentId { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public string Level { get; set; }
		public bool IsRegularSeason { get; set; }
	}
}
