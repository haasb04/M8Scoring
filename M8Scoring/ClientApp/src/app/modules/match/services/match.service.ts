import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ReplaySubject } from 'rxjs';

@Injectable()
export class MatchService {
  matchId: number;
  public match: Match;
  public working: boolean;
  public fifthSetResults: ReplaySubject<FifthSetResults> = new ReplaySubject(1);

  constructor(private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: any,
    @Inject('BASE_URL') private baseUrl: string) {
  }

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

  getMatch(id: number): any {
    //retreive match from the server
    this.working = true;
    var url = this.baseUrl + "api/match/" + id;

    return this.http.get<Match>(url).map(
      (res) => {
        this.match = res;
        this.working = false;
        return this.match;
      }, error => console.log(error));
  }

  calculateMatch() {
    this.calculate(this.match);
    this.determineFifthSetScenarios(this.match);


  }

  determineFifthSetScenarios(match: Match) {
    if (match.Set1.Player1.Player != null && match.Set1.Player2.Player != null &&
      match.Set2.Player1.Player != null && match.Set2.Player2.Player != null &&
      match.Set3.Player1.Player != null && match.Set3.Player2.Player != null &&
      match.Set4.Player1.Player != null && match.Set4.Player2.Player != null &&
      match.Set5.Player1.Player != null && match.Set5.Player2.Player != null
    ) {
      //fifth set scenarios
      var results = <FifthSetResults>{};
      results.MustGet = this.determineMustGet();
      results.MustHold = this.determineHoldThemTo();
      this.fifthSetResults.next(results);
    }
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

    if (set.Player1.Player == null || set.Player2.Player == null) {
      //dont calculate
      return [0, 0];
    }

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
    let addOn: number = 0;
    let opponentAddOn: number = 0;
    let setTotal: number = 0;
    let opponentSetTotal: number = 0;

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
        addOn = delta * match.WinMultiplier;
        score += addOn;
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
        opponentAddOn = delta * match.WinMultiplier;
        opponentScore += opponentAddOn;
        opponentMargin = delta;
      }
    }
    set.Margin = margin;
    set.OpponentMargin = opponentMargin;
    set.WinBonus = winBonus;
    set.OpponentWinBonus = opponentWinBonus;
    set.Multiplier = match.WinMultiplier;
    set.AddOn = addOn;
    set.OpponentAddOn = opponentAddOn;
    set.SetTotal = score;
    set.OpponentSetTotal = opponentScore;

    return [score, opponentScore];
  }

  determineMustGet() {
    //Clone the match
    //Set fifth match score to rate and set the win flag.
    //For opponent score = 0; opponent score < their rate +15
    ////if their match score > our match score, we lose.
    ////Must hold oponent to one less than this ball.
    var scenarioResult: number[] = new Array(16);
    var mustGetScore: number = 0;

    var testMatch = JSON.parse(JSON.stringify(this.match));

    var effectiveRate: number;
    var effectiveOpponentRate: number;

    if (testMatch.Set5.Player1.Rate == -1 || testMatch.Set5.Player2.Rate == -1) {
      effectiveRate = effectiveOpponentRate = 50;
    } else {
      effectiveRate = testMatch.Set5.Player1.Rate;
      effectiveOpponentRate = testMatch.Set5.Player2.Rate;
    }

    scenarioResult[0] = effectiveOpponentRate;
    var i: number;
    for (i = 0; i < 15; i++) {
      testMatch.Set5.Player2.Score = effectiveOpponentRate + i;

      testMatch.Set5.Win = false;
      mustGetScore = 0;
      var myScore: number;
      var lastTopScore = 0;
      for (myScore = lastTopScore; myScore <= effectiveRate + 15; myScore++) {
        testMatch.Set5.Player1.Score = myScore;
        this.calculate(testMatch);
        if (testMatch.TotalOpponentScore < testMatch.TotalScore) {
          mustGetScore = myScore;
          lastTopScore = myScore;
          break;
        }
      }

      scenarioResult[i + 1] = mustGetScore;
      //console.log("Opponent Score: " + testMatch.Set5.Player2.Score + ", must get to: " + mustGetScore);
    }

    return scenarioResult;
  }

  determineHoldThemTo() {
    //Clone match
    //Set fifth match score to rate and  set the win flag
    //For opponent score = 0; opponent score < their rate +14
    //If their match score > our match score, we lose.
    //Must hold opponent to one less than this ball.
    var scenarioResult: number[] = new Array(16);
    var maxOpponentScore: number = 0;

    var testMatch = JSON.parse(JSON.stringify(this.match)); //{ ...this.match };

    var effectiveRate: number;
    var effectiveOpponentRate: number;

    if (testMatch.Set5.Player1.Rate == -1 || testMatch.Set5.Player2.Rate == -1) {
      effectiveRate = effectiveOpponentRate = 50;
    } else {
      effectiveRate = testMatch.Set5.Player1.Rate;
      effectiveOpponentRate = testMatch.Set5.Player2.Rate;
    }

    scenarioResult[0] = effectiveRate;
    //find the numbers;
    var i: number;
    for (i = 0; i < 15; i++) {
      testMatch.Set5.Player1.Score = effectiveRate + i;

      testMatch.Set5.Win = true;
      maxOpponentScore = 0;

      var oScore: number;
      var oLastTop: number = 0;

      for (oScore = oLastTop; oScore <= effectiveOpponentRate + 15; oScore++) {
        testMatch.Set5.Player2.Score = oScore;
        this.calculate(testMatch);
        if (testMatch.TotalOpponentScore >= testMatch.TotalScore) {
          maxOpponentScore = oScore - 1;
          oLastTop = maxOpponentScore;
          break;
        }
      }

      scenarioResult[i + 1] = maxOpponentScore;
      console.log("Score: " + testMatch.Set5.Player1.Score + ", must hold opponent to: " + maxOpponentScore);
    }

    return scenarioResult;
  }
}
