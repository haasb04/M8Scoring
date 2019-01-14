using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	/// <summary>
	/// Used to deserialzied PARAMETER info sent FROM client
	/// </summary>
	public class SortFilterParameter {

		#region Constructors
		public SortFilterParameter() {

		}

		#endregion

		#region Properties
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
		public object[] Data { get; set; }
		#endregion


	}
}
