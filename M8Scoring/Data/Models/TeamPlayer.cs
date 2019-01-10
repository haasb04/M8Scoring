using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class TeamPlayer {
		#region "Properties"
		public int TeamId { get; set; }
		public Team Team { get; set; }
		public int PlayerId { get; set; }
		public Player Player { get; set; }
		#endregion
	}
}
