using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace M8Scoring.ViewModels {
	public class TeamListViewModel : SpfListViewModelBase {
		#region Constructors
		public TeamListViewModel() {	}

		public TeamListViewModel(string spfJson): base(spfJson) {

		}
		#endregion

		#region Properties

		public TeamViewModel[] Data { get; set; }

		#endregion

		#region Methods
		public void PrepareData(IQueryable<Team> DbSet) {
			
			IQueryable<TeamViewModel> teams = DbSet.Select(t => new TeamViewModel { Id = t.Id, Name = t.Name, Number = t.Number });

			if(!string.IsNullOrEmpty(SpfInput.Filter)) {
				int termAsInt = -1;
				int.TryParse(SpfInput.Filter, out termAsInt);

				teams = teams.Where(t => t.Name.Contains(SpfInput.Filter) || t.Number == termAsInt);
			}

			switch(SpfInput.SortCol) {
				case "Id":
					if(SpfInput.SortOrder) {
						teams = teams.OrderByDescending(t => t.Id);
					} else {
						teams = teams.OrderBy(t => t.Id);
					}
					break;
				case "Name":
					if(SpfInput.SortOrder) {
						teams = teams.OrderByDescending(t => t.Name);
					} else {
						teams = teams.OrderBy(t => t.Name);
					}
					break;
				case "Number":
					if(SpfInput.SortOrder) {
						teams = teams.OrderByDescending(t => t.Number);
					} else {
						teams = teams.OrderBy(t => t.Number);
					}
					break;
				default:
					teams = teams.OrderBy(t => t.Id);
					break;
			}

			PaginatedList<TeamViewModel> list = new PaginatedList<TeamViewModel>(teams, SpfInput.PageIndex ?? 0, SpfInput.PageSize);
			SetSpfOutput<TeamViewModel>(list);
			Data = list.ToArray(); 
		}
		#endregion
	}
}
