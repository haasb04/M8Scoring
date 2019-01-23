using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M8Scoring.Data;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace M8Scoring.ViewModels {
	public class PlayerListViewModel : SpfListViewModelBase {
		#region Constructors
		public PlayerListViewModel() { }

		public PlayerListViewModel(string spfJson) : base(spfJson) {

		}
		#endregion

		#region Properties

		public PlayerViewModel[] Data { get; set; }

		#endregion

		#region Methods
		public void PrepareData(IQueryable<Player> DbSet) {

			IQueryable<PlayerViewModel> players = DbSet.Select(t => new PlayerViewModel { Id = t.Id, FirstName = t.FirstName, LastName = t.LastName, Number = t.Number });

			if(!string.IsNullOrEmpty(SpfInput.Filter)) {
				int termAsInt = -1;
				int.TryParse(SpfInput.Filter, out termAsInt);

				players = players.Where(t => t.FirstName.Contains(SpfInput.Filter) ||
																			t.LastName.Contains(SpfInput.Filter) ||
																			t.Number == termAsInt);
			}

			switch(SpfInput.SortCol) {
				case "Id":
					if(SpfInput.SortOrder) {
						players = players.OrderByDescending(t => t.Id);
					} else {
						players = players.OrderBy(t => t.Id);
					}
					break;
				case "FirstName":
					if(SpfInput.SortOrder) {
						players = players.OrderByDescending(t => t.FirstName);
					} else {
						players = players.OrderBy(t => t.FirstName);
					}
					break;
				case "LastName":
					if(SpfInput.SortOrder) {
						players = players.OrderByDescending(t => t.LastName);
					} else {
						players = players.OrderBy(t => t.LastName);
					}
					break;
				case "Number":
					if(SpfInput.SortOrder) {
						players = players.OrderByDescending(t => t.Number);
					} else {
						players = players.OrderBy(t => t.Number);
					}
					break;
				default:
					players = players.OrderBy(t => t.Id);
					break;
			}

			PaginatedList<PlayerViewModel> list = new PaginatedList<PlayerViewModel>(players, SpfInput.PageIndex ?? 0, SpfInput.PageSize);
			SetSpfOutput<PlayerViewModel>(list);
			this.SpfOutput.Filtered = !string.IsNullOrEmpty(this.SpfInput.Filter);
			Data = list.ToArray();
		}
		#endregion
	}
}
