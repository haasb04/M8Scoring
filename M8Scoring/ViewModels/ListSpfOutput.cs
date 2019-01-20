using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	/// <summary>
	/// 
	/// </summary>
	public class ListSpfOutput {

		#region Constructors
		public ListSpfOutput() {

		}

		#endregion

		#region Properties
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
		public int PageIndex { get; set; }
		public bool HasPreviousPage { get; set; }
		public bool HasNextPage { get; set; }
		public bool Filtered { get; set; }
		#endregion


	}
}
