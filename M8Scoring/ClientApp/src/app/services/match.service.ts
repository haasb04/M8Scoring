import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class MatchService {
  matchId: number;
  public match: Match;

  constructor(private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: any) {
    this.getMatch();
  }

  createMatch(myTeam: Team, opponent: Team) {
    //server.CreateMatch(myTeam.Id, opponent.Id); 
    this.match = <Match>{};
    this.match.Id = 1002;
    this.match.Opponent = opponent
    this.match.Opponent.Players = [];// <Player[]>[{Id=1, Name='Brian', Rate=55}];
    var p1 = <Player>{};
    p1.Id = 1;
    p1.Name = 'Brian';
    p1.Rate = 55;
    this.match.Opponent.Players.push(p1);


  };
 
  getMatch() {
      //create a new match
      //server creates the match, and provides a new id

    this.createMatch(<Team>{}, <Team>{});
  }
}
