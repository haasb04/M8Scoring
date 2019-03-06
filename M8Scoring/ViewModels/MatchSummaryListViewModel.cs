using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;

namespace M8Scoring.ViewModels {
	public class MatchSummaryListViewModel : SpfListViewModelBase {
		#region Constructors
		public MatchSummaryListViewModel() { }

		public MatchSummaryListViewModel(string spfJson):base(spfJson) {

		}

		#endregion

		#region Properties
		public MatchSummaryViewModel[] Data { get; set; }

		#endregion

		#region Methods
		public void PrepareData(IQueryable<Match> DbSet) {
			IQueryable<MatchSummaryViewModel> matches = DbSet.Select(m => new MatchSummaryViewModel { Id = m.Id, Date = m.Date.ToShortDateString(), Opponent = m.Opponent.Name, Score = string.Format("{0} - {1}", m.TotalScore, m.TotalOpponentScore) });

			if(!string.IsNullOrEmpty(SpfInput.Filter)) {
				int termAsInt = -1;
				int.TryParse(SpfInput.Filter, out termAsInt);

				matches = matches.Where(m => m.Id == termAsInt || m.Opponent.Contains(SpfInput.Filter) || m.Date.Contains(SpfInput.Filter));
			}

			switch(SpfInput.SortCol) {
				case "Id":
					if(SpfInput.SortOrder) {
						matches = matches.OrderByDescending(m => m.Id);
					} else {
						matches = matches.OrderBy(m => m.Id);
					}
					break;
				case "Opponent":
					if(SpfInput.SortOrder) {
						matches = matches.OrderByDescending(m => m.Opponent);
					} else {
						matches = matches.OrderBy(m => m.Opponent);
					}
					break;
				case "Date":
					if(SpfInput.SortOrder) {
						matches = matches.OrderByDescending(m => DateTime.Parse(m.Date));
					} else {
						matches = matches.OrderBy(m => DateTime.Parse(m.Date));
					}
					break;
				default:
					matches = matches.OrderByDescending(m => m.Id);
					break;
			}

			PaginatedList<MatchSummaryViewModel> list = new PaginatedList<MatchSummaryViewModel>(matches, SpfInput.PageIndex ?? 0, SpfInput.PageSize);
			SetSpfOutput<MatchSummaryViewModel>(list);
			this.SpfOutput.Filtered = !string.IsNullOrEmpty(this.SpfInput.Filter);
			Data = list.ToArray();
		}
		#endregion
	}
}
