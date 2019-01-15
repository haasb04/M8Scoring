using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Newtonsoft.Json;

namespace M8Scoring.ViewModels {

	public class SpfListViewModelBase {
		public SpfListViewModelBase() {
			SpfOutput = new ListSpfOutput();
		}

		public SpfListViewModelBase(string spfJson) {
			try {
				SpfInput = JsonConvert.DeserializeObject(spfJson).Adapt<ListSpfInput>();
				//defaults
				if(SpfInput.PageSize == 0) {
					SpfInput.PageSize = 10;
				}
			} catch(JsonSerializationException) {
				//TODO: Log this exception
				throw;
			}
		}

		protected void SetSpfOutput<T>(PaginatedList<T> list) {
			SpfOutput = list.Adapt<ListSpfOutput>();
			//if(SpfOutput == null) {
			//	SpfOutput = new ListSpfOutput();
			//}

			//SpfOutput.PageIndex = list.PageIndex;
			//SpfOutput.PageSize = list.PageSize;
			//SpfOutput.TotalCount = list.TotalCount;
			//SpfOutput.TotalPages = list.TotalPages;
			//SpfOutput.HasNextPage = list.HasNextPage;
			//SpfOutput.HasPreviousPage = list.HasPreviousPage;
		}

		public ListSpfOutput SpfOutput{ get; set; }
		[JsonIgnore]
		public ListSpfInput SpfInput { get; set; }
	}
}
