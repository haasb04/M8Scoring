import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { last } from '@angular/router/src/utils/collection';

@Injectable()
export class MatchService {
  matchId: number;
  public match: Match;
  public working: boolean;

  constructor(private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: any,
    @Inject('BASE_URL') private baseUrl: string) {
    //this.createMatch(1, 2);
  }

  createMatch(myTeamId: number, opponentId: number) {
    //server.CreateMatch(myTeam.Id, opponent.Id); 

    this.match = <Match>{};

    //default settings
    this.match.WinMultiplier = 3;
    this.match.OverUnderPenalty = 325;
    this.match.PenaltyMultiplier = 5;
    this.match.NonRatedPlayerRate = 50;
    this.match.IsRegularSeason = true;

    this.match.Id = 1002;
    this.match.Date = new Date(Date.now()).toJSON();
    this.match.Number = 1;
    this.match.TotalScore = 0;
    this.match.TotalOpponentScore = 0;
    this.match.TeamBonusOrPenalty = 0;
    this.match.OpponentBonusOrPenalty = 0;
    //create opponent team
    var opponent = <Team>{};
    //opponent.Id = 2;
    opponent.Number = 76101;
    opponent.Name = "Wolf Pack";

    var opponentList: Player[] = [
      <Player>{ Id: 1, Number: 33739, FirstName: 'McEathron, Darren', Rate: 73 },
      <Player>{ Id: 2, Number: 8978, FirstName: 'Kamman, Brian', Rate: 44 },
      <Player>{ Id: 3, Number: 11041, FirstName: 'Souba, Todd', Rate: 96 },
      <Player>{ Id: 4, Number: 14305, FirstName: 'Samson, James', Rate: 46 },
      <Player>{ Id: 5, Number: 18985, FirstName: 'Dave, Krutz', Rate: 82 },
      <Player>{ Id: 6, Number: 34331, FirstName: 'Matlock, Christopher', Rate: 54 },
      <Player>{ Id: 7, Number: 38208, FirstName: 'Valenzuela, Joe', Rate: 63 }
    ];
    opponent.Players = opponentList;
    this.match.Opponent = opponent;

    var myTeam = <Team>{};
   // myTeam.Id = 1;
    myTeam.Number = 76102;
    myTeam.Name = "Holy Rollers";

    var myList: Player[] = [
      <Player>{ Id: 8, Number: 35560, FirstName: 'Williams, Chase', Rate: 54 },
      <Player>{ Id: 9, Number: 14798, FirstName: 'Forar, Brandon', Rate: 98 },
      <Player>{ Id: 10, Number: 23704, FirstName: 'Hebig, Travis', Rate: 49 },
      <Player>{ Id: 11, Number: 35697, FirstName: 'Gregerson, Brian', Rate: 68 },
      <Player>{ Id: 12, Number: 37377, FirstName: 'Haas, Brian', Rate: 55 },
      <Player>{ Id: 13, Number: 37378, FirstName: 'Wolf, Cory', Rate: 72 },
      <Player>{ Id: 14, Number: 37770, FirstName: 'Forar, Nick', Rate: 64 }
    ];
    myTeam.Players = myList;
    this.match.Team = myTeam;

    //create sets.  These will be created by server
    this.match.Set1 = <MatchSet>{ Id: 1, SetNumber: 1 };
    this.match.Set2 = <MatchSet>{ Id: 2, SetNumber: 2 };
    this.match.Set3 = <MatchSet>{ Id: 3, SetNumber: 3 };
    this.match.Set4 = <MatchSet>{ Id: 4, SetNumber: 4 };
    this.match.Set5 = <MatchSet>{ Id: 5, SetNumber: 5 };

    this.match.Set1.Player1 = <MatchSetPlayer>{ Rate: 0, Score: 0 };
    this.match.Set2.Player1 = <MatchSetPlayer>{ Rate: 0, Score: 0 };
    this.match.Set3.Player1 = <MatchSetPlayer>{ Rate: 0, Score: 0 };
    this.match.Set4.Player1 = <MatchSetPlayer>{ Rate: 0, Score: 0 };
    this.match.Set5.Player1 = <MatchSetPlayer>{ Rate: 0, Score: 0 };

    this.match.Set1.Player2 = <MatchSetPlayer>{ Rate: 0, Score: 0 };
    this.match.Set2.Player2 = <MatchSetPlayer>{ Rate: 0, Score: 0 };
    this.match.Set3.Player2 = <MatchSetPlayer>{ Rate: 0, Score: 0 };
    this.match.Set4.Player2 = <MatchSetPlayer>{ Rate: 0, Score: 0 };
    this.match.Set5.Player2 = <MatchSetPlayer>{ Rate: 0, Score: 0 };

    //TEMP
    var url = this.baseUrl + "api/match/"
    this.http.put<Match>(url, this.match).subscribe(
      res => { var v = res;}, error => console.log(error)
    );
    return this.match;
  };

  saveMatch() {
    if (this.match == null) {
      return;
    }
    this.working = true;
    var url = this.baseUrl + "api/match/";
    this.http.post<boolean>(url, this.match).subscribe(
      res => {
        let r = res;
        this.working = false;
      }, error => console.log(error)
    );
  }

  getMatch(id: number):any {
    //retreive match from the server
    this.working = true;
    var url = this.baseUrl + "api/match/"+id;

    return this.http.get<Match>(url).map(
      (res) => {
        this.match = res;
        this.working = false;
        return this.match;
      }, error => console.log(error));
  }

  calculateMatch() {
    this.calculate(this.match);
  }

  calculate(match: Match) {
    //sum all sets
    let totalPlayerRatings: number = 0;
    let totalOpponentPlayerRatings: number = 0;
    let teamScore: number = 0;
    let opponentScore: number = 0;
    let weForfeit: boolean = false;
    let theyForfeit: boolean = false;
    let unPlayedSet: boolean = false;

    var set1Results = this.calculateSetScore(match, match.Set1);
    weForfeit = weForfeit || match.Set1.Player1.Forfeit;
    theyForfeit = theyForfeit || match.Set1.Player2.Forfeit;
    teamScore += set1Results[0];
    opponentScore += set1Results[1];
    unPlayedSet = unPlayedSet || (match.Set1.Player1.Rate == 0 && match.Set1.Player2.Rate == 0);
    totalPlayerRatings += (match.Set1.Player1.Rate == -1 ? 50 : match.Set1.Player1.Rate);
    totalOpponentPlayerRatings += (match.Set1.Player2.Rate == -1 ? 50 : match.Set1.Player2.Rate);

    var set2Results = this.calculateSetScore(match, match.Set2);
    weForfeit = weForfeit || match.Set2.Player1.Forfeit;
    theyForfeit = theyForfeit || match.Set2.Player2.Forfeit;
    teamScore += set2Results[0];
    opponentScore += set2Results[1];
    unPlayedSet = unPlayedSet || (match.Set2.Player1.Rate == 0 && match.Set2.Player2.Rate == 0);
    totalPlayerRatings += (match.Set2.Player1.Rate == -1 ? 50 : match.Set2.Player1.Rate);
    totalOpponentPlayerRatings += (match.Set2.Player2.Rate == -1 ? 50 : match.Set2.Player2.Rate);

    var set3Results = this.calculateSetScore(match, match.Set3);
    weForfeit = weForfeit || match.Set3.Player1.Forfeit;
    theyForfeit = theyForfeit || match.Set3.Player2.Forfeit;
    teamScore += set3Results[0];
    opponentScore += set3Results[1];
    unPlayedSet = unPlayedSet || (match.Set3.Player1.Rate == 0 && match.Set3.Player2.Rate == 0);
    totalPlayerRatings += (match.Set3.Player1.Rate == -1 ? 50 : match.Set3.Player1.Rate);
    totalOpponentPlayerRatings += (match.Set3.Player2.Rate == -1 ? 50 : match.Set3.Player2.Rate);

    var set4Results = this.calculateSetScore(match, match.Set4);
    weForfeit = weForfeit || match.Set4.Player1.Forfeit;
    theyForfeit = theyForfeit || match.Set4.Player2.Forfeit;
    teamScore += set4Results[0];
    opponentScore += set4Results[1];
    unPlayedSet = unPlayedSet || (match.Set4.Player1.Rate == 0 && match.Set4.Player2.Rate == 0);
    totalPlayerRatings += (match.Set4.Player1.Rate == -1 ? 50 : match.Set4.Player1.Rate);
    totalOpponentPlayerRatings += (match.Set4.Player2.Rate == -1 ? 50 : match.Set4.Player2.Rate);

    var set5Results = this.calculateSetScore(match, match.Set5);
    weForfeit = weForfeit || match.Set5.Player1.Forfeit;
    theyForfeit = theyForfeit || match.Set5.Player2.Forfeit;
    teamScore += set5Results[0];
    opponentScore += set5Results[1];
    unPlayedSet = unPlayedSet || (match.Set5.Player1.Rate == 0 && match.Set5.Player2.Rate == 0);
    totalPlayerRatings += (match.Set5.Player1.Rate == -1 ? 50 : match.Set5.Player1.Rate);
    totalOpponentPlayerRatings += (match.Set5.Player2.Rate == -1 ? 50 : match.Set5.Player2.Rate);

    //calculate over/under
    //no bonus awarded to either team if a set is unplayed
    match.TotalRate = totalPlayerRatings;
    match.OpponentTotalRate = totalOpponentPlayerRatings;
    if (!unPlayedSet) {
      if (totalPlayerRatings > match.OverUnderPenalty) {
        match.TeamBonusOrPenalty = -(totalPlayerRatings - match.OverUnderPenalty) * match.PenaltyMultiplier;
      } else {
        match.TeamBonusOrPenalty = weForfeit ? 0 : (match.OverUnderPenalty - totalPlayerRatings);
      }


      teamScore += match.TeamBonusOrPenalty;

      if (totalOpponentPlayerRatings > match.OverUnderPenalty) {
        match.OpponentBonusOrPenalty = -(totalOpponentPlayerRatings - match.OverUnderPenalty) * match.PenaltyMultiplier;
      } else {
        match.OpponentBonusOrPenalty = theyForfeit ? 0 : (match.OverUnderPenalty - totalOpponentPlayerRatings);
      }

      opponentScore += match.OpponentBonusOrPenalty;
    }
    match.TotalScore = teamScore;
    match.TotalOpponentScore = opponentScore;
    console.log('us:' + match.TotalScore + '  them:' + match.TotalOpponentScore);
  }

  calculateSetScore(match: Match, set: MatchSet) {
    let score: number = set.Player1.Score;
    let opponentScore: number = set.Player2.Score;
    let effectiveRate = set.Player1.Rate;
    let effectiveOpponentRate = set.Player2.Rate;
    let noRate: boolean = false;
    let noOpponentRate: boolean = false;

    //forfeited match
    let forfeit: boolean = false;
    if (set.Player1.Forfeit) {
      //we forfeit
      //regular season - 150 points, postseason -250 points
      opponentScore = (match.IsRegularSeason ? 150 : 250);
      score = 0;
      forfeit = true;
    }
    if (set.Player2.Forfeit) {
      //they forfeit
      //regular season - 150 points, postseason - 250 points
      score = (match.IsRegularSeason ? 150 : 250);
      opponentScore = 0;
      forfeit = true;
    }

    if (forfeit) {
      return [score, opponentScore];
    }

    //unrated players. race to 50.
    if (effectiveRate == -1) {
      effectiveRate = 50;
      effectiveOpponentRate = 50;
      noRate = true;
    }

    if (effectiveOpponentRate == -1) {
      effectiveOpponentRate = 50;
      effectiveRate = 50;
      noOpponentRate = true;
    }

    let margin: number = 0;
    let opponentMargin: number = 0;
    let winBonus: number = 0;
    let opponentWinBonus: number = 0;

    if (set.Win) {
      if (set.Player1.Score > 0) {
        score += 100;
        winBonus = 100;
      }
      let delta: number = 0;
      if (noOpponentRate) {
        delta = 50 - set.Player2.Score;
      } else {
        delta = effectiveOpponentRate - set.Player2.Score;
      }
      if (delta > 0) {
        score += (delta * match.WinMultiplier);
        margin = delta;
      }
    } else {
      if (set.Player2.Score > 0) {
        opponentScore += 100;
        opponentWinBonus = 100;
      }
      let delta: number = 0;
      if (noRate) {
        delta = 50 - set.Player1.Score;
      } else {
        delta = effectiveRate - set.Player1.Score;
      }
      if (delta > 0) {
        opponentScore += (delta * match.WinMultiplier);
        opponentMargin = delta;
      }
    }
    return [score, opponentScore];


  }
}