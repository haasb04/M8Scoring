using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	public class PlayerViewModel {
		#region Constructors
		public PlayerViewModel() {

		}
		#endregion

		#region Properties
		public int Number { get; set; }
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Rating { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
		#endregion
	}
}
