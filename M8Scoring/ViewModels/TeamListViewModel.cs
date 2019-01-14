using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace M8Scoring.ViewModels {
	public class TeamListViewModel : SortFilterBase {
		#region Constructors
		public TeamListViewModel() {	}
		#endregion

		#region Properties

		[JsonIgnore()]
		public DbSet<Team> DbSet { get; set; }
		#endregion

		#region Methods
		public void PrepareData() {
			
			IQueryable<TeamViewModel> teams = DbSet.Select(t => new TeamViewModel { Id = t.Id, Name = t.Name, Number = t.Number });

			if(!string.IsNullOrEmpty(Filter)) {
				int termAsInt = -1;
				int.TryParse(Filter, out termAsInt);

				teams = teams.Where(t => t.Name.Contains(Filter) || t.Number == termAsInt);
			}

			switch(SortCol) {
				case "Id":
					if(SortOrder) {
						teams = teams.OrderByDescending(t => t.Id);
					} else {
						teams = teams.OrderBy(t => t.Id);
					}
					break;
				case "Name":
					if(SortOrder) {
						teams = teams.OrderByDescending(t => t.Name);
					} else {
						teams = teams.OrderBy(t => t.Name);
					}
					break;
				case "Number":
					if(SortOrder) {
						teams = teams.OrderByDescending(t => t.Number);
					} else {
						teams = teams.OrderBy(t => t.Number);
					}
					break;
				default:
					teams = teams.OrderBy(t => t.Id);
					break;
			}

			PaginatedList<TeamViewModel> list = new PaginatedList<TeamViewModel>(teams, PageIndex ?? 0, PageSize);
			this.SetReturnData(list);
			Data = list.ToArray(); 
		}
		#endregion
	}
}
