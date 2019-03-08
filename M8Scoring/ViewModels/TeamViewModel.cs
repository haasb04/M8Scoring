using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	public class TeamViewModel {
		#region "Constructors"
		public TeamViewModel() {

		}
		#endregion

		#region "Properties"
		public int Id { get; set; }
		public string Name { get; set; }
		public int Number { get; set; }
		public string Home { get; set; }
		public string Level { get; set; }
		public DateTime LastModifiedDate { get; set; }
		public DateTime CreatedDate { get; set; }
		public PlayerViewModel[] Players { get; set; }
		#endregion
	}
}
