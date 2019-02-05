using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class SubscriptionTeam {

		public SubscriptionTeam() {

		}

		#region Properties
		public int SubscriptionId { get; set; }
		public int TeamId { get; set; }
		public Subscription Subscription { get; set; }
		public Team Team { get; set; }
		public string TeamLogin { get; set; }
		public string TeamPassword { get; set; }
		#endregion

	}
}
