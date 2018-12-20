interface Match {
  Id: number;
  Number: number;
  Date: Date | string;
  Team: Team;
  Opponent: Team;
  IsComplete: boolean;
  Set1: MatchSet;
  Set2: MatchSet;
  Set3: MatchSet;
  Set4: MatchSet;
  Set5: MatchSet;
}
