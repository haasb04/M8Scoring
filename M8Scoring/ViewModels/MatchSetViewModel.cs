using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	public class MatchSetViewModel {

		public MatchSetViewModel() {

		}

		public int Id { get; set; }
		public int SetNumber { get; set; }

		public virtual MatchSetPlayerViewModel Player1 { get; set; }
		public virtual MatchSetPlayerViewModel Player2 { get; set; }

		public bool Win { get; set; }
	}
}
