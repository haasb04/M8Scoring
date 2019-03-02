interface Match {
  Id: number;
  Number: number;
  Date: string;
  Team: Team;
  Opponent: Team;
  IsComplete: boolean;
  Set1: MatchSet;
  Set2: MatchSet;
  Set3: MatchSet;
  Set4: MatchSet;
  Set5: MatchSet;
  //settings
  WinMultiplier: number;
  OverUnderPenalty: number;
  PenaltyMultiplier: number;
  NonRatedPlayerRate: number;
  IsRegularSeason: boolean;

  //calcualted values
  TotalScore: number;
  TotalOpponentScore: number;
  TotalRate: number;
  OpponentTotalRate: number;
  TeamBonusOrPenalty: number;
  OpponentBonusOrPenalty: number;

}
