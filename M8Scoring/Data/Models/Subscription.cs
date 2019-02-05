using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class Subscription {

		public Subscription() {

		}

		#region Properties
		public int Id { get; set; }

		[ForeignKey("ApplicationUser")]
		public string UserId { get; set; }

		public ICollection<SubscriptionTeam> SubscriptionTeams { get; } = new List<SubscriptionTeam>();
		#endregion

		#region Lazy Loaded Properties
		public virtual ApplicationUser User { get; set; }
		#endregion
	}
}
