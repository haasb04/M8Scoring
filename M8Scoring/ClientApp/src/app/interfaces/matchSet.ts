interface MatchSet {
  Id: number;
  SetNumber: number;
  Player1: MatchSetPlayer;
  Player2: MatchSetPlayer;
  Win: boolean;

  Margin: number;
  OpponentMargin: number;
  WinBonus: number;
  OpponentWinBonus;
  AddOn: number;
  OpponentAddOn: number;
  Multiplier: number;
  SetTotal: number;
  OpponentSetTotal: number;
}


