using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace M8Scoring.Data {
	public class Match {
		#region Constructors

		#endregion

		#region Properties
		[Key]
		[Required]
		public int Id { get; set; }
		public int Number { get; set; }
		public DateTime Date { get; set; }
		public int WinMultiplier { get; set; }
		public int OverUnderPenalty { get; set; }
		public int PenaltyMultiplier { get; set; }
		public int NonRatedPlayerRate { get; set; }
		public bool IsRegularSeason { get; set; }
		public string Location { get; set; }

		//calculated Totals
		public int TotalScore { get; set; }
		public int TotalOpponentScore { get; set; }
		public int TotalRate { get; set; }
		public int TotalOpponentRate { get; set; }
		public int TeamBonusOrPenalty { get; set; }
		public int OpponentBonusOrPenalty { get; set; }

		public int? TeamId { get; set; }
		public virtual Team Team { get; set; }

		public int? OpponentId { get; set; }
		public virtual Team Opponent { get; set; }

		public List<MatchSet> Sets { get; set; } = new List<MatchSet>();

		public MatchSet Set1 {
			get {
				return Sets.Where(s => s.SetNumber == 1).FirstOrDefault();
			}
		}

		public MatchSet Set2 {
			get {
				return Sets.Where(s => s.SetNumber == 2).FirstOrDefault();
			}
		}

		public MatchSet Set3 {
			get {
				return Sets.Where(s => s.SetNumber == 3).FirstOrDefault();
			}
		}

		public MatchSet Set4 {
			get {
				return Sets.Where(s => s.SetNumber == 4).FirstOrDefault();
			}
		}

		public MatchSet Set5 {
			get {
				return Sets.Where(s => s.SetNumber == 5).FirstOrDefault();
			}
		}

		public DateTime CreatedDate { get; set; }
		public DateTime LastModifiedDate { get; set; }
		#endregion
		internal static Match CreateAdvancedMatch(bool isRegularSeason) {
			Match match = new Match();

			match.WinMultiplier = 3;
			match.OverUnderPenalty = 325;
			match.PenaltyMultiplier = 5;
			match.NonRatedPlayerRate = 50;
			match.IsRegularSeason = isRegularSeason;

			match.CreatedDate = DateTime.Now;
			match.LastModifiedDate = match.CreatedDate;

			match.Sets.Add(new MatchSet(match) { SetNumber = 1 });
			match.Sets.Add(new MatchSet(match) { SetNumber = 2 });
			match.Sets.Add(new MatchSet(match) { SetNumber = 3 });
			match.Sets.Add(new MatchSet(match) { SetNumber = 4 });
			match.Sets.Add(new MatchSet(match) { SetNumber = 5 });

			match.Set1.Players.Add(new MatchSetPlayer(match.Set1) { Number = 1 });
			match.Set1.Players.Add(new MatchSetPlayer(match.Set1) { Number = 2 });

			match.Set2.Players.Add(new MatchSetPlayer(match.Set2) { Number = 1 });
			match.Set2.Players.Add(new MatchSetPlayer(match.Set2) { Number = 2 });

			match.Set3.Players.Add(new MatchSetPlayer(match.Set3) { Number = 1 });
			match.Set3.Players.Add(new MatchSetPlayer(match.Set3) { Number = 2 });


			match.Set4.Players.Add(new MatchSetPlayer(match.Set4) { Number = 1 });
			match.Set4.Players.Add(new MatchSetPlayer(match.Set4) { Number = 2 });

			match.Set5.Players.Add(new MatchSetPlayer(match.Set5) { Number = 1 });
			match.Set5.Players.Add(new MatchSetPlayer(match.Set5) { Number = 2 });

			return match;
		}
	}
}
