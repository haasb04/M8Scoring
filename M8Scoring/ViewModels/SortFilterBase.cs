using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.ViewModels {
	public abstract class SortFilterBase {
		public string SortCol { get; set; }
		public bool SortOrder { get; set; }
		public string Filter { get; set; }
		public int? PageIndex { get; set; }
		public int PageSize { get; set; } = 10;
		public int TotalPages { get; set; }
		public int TotalCount { get; set; }
		public bool HasPreviousPage { get; set; }
		public bool HasNextPage { get; set; }
		public object[] Data { get; set; }

		public void SetReturnData<T>(PaginatedList<T> list) {
			TotalPages = list.TotalPages;
			TotalCount = list.TotalCount;
			HasPreviousPage = list.HasPreviousPage;
			HasNextPage = list.HasNextPage;
		}

	}
}
