using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace M8Scoring.ViewModels {
	public class ListSpfInput {

		public ListSpfInput() {
			PageSize = 10;
			PageIndex = 0;
			Filter = "";
			SortCol = "";
		}

		public string SortCol { get; set; }
		public bool SortOrder { get; set; }
		public string Filter { get; set; }
		public int? PageIndex { get; set; }
		public int PageSize { get; set; } = 10;
	
	}
}
