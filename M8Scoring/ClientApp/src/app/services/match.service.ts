import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { last } from '@angular/router/src/utils/collection';

@Injectable()
export class MatchService {
  matchId: number;
  public match: Match;

  constructor(private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: any) {
    this.createMatch(1,2);
  }

  createMatch(myTeamId: number, opponentId: number) {
    //server.CreateMatch(myTeam.Id, opponent.Id); 

    this.match = <Match>{};
    this.match.Id = 1002;
    this.match.Date = Date.now.toString();
    this.match.Number = 1;
   
    //create opponent team
    var opponent = <Team>{};
    opponent.Id = 2;
    opponent.Number = 76101;
    opponent.Name = "Wolf Pack";

    var opponentList: Player[] = [
      { Id: 1, Number: 33739, Name: 'McEathron, Darren', Rate: 73 },
      { Id: 2, Number: 8978, Name: 'Kamman, Brian', Rate: 44 },
      { Id: 3, Number: 11041, Name: 'Souba, Todd', Rate: 96 },
      { Id: 4, Number: 14305, Name: 'Samson, James', Rate: 46 },
      { Id: 5, Number: 18985, Name: 'Dave, Krutz', Rate: 82 },
      { Id: 6, Number: 34331, Name: 'Matlock, Christopher', Rate: 54 },
      { Id: 7, Number: 38208, Name: 'Valenzuela, Joe', Rate: 63 }
    ];
    opponent.Players = opponentList;
    this.match.Opponent = opponent;

    var myTeam = <Team>{};
    myTeam.Id = 1;
    myTeam.Number = 76102;
    myTeam.Name = "Holy Rollers";
    
    var myList: Player[] = [
      { Id: 8, Number: 35560, Name: 'Williams, Chase', Rate: 54 },
      { Id: 9, Number: 14798, Name: 'Forar, Brandon', Rate: 98 },
      { Id: 10, Number: 23704, Name: 'Hebig, Travis', Rate: 49 },
      { Id: 11, Number: 35697, Name: 'Gregerson, Brian', Rate: 68 },
      { Id: 12, Number: 37377, Name: 'Haas, Brian', Rate: 55 },
      { Id: 13, Number: 37378, Name: 'Wolf, Cory', Rate: 72 },
      { Id: 14, Number: 37770, Name: 'Forar, Nick', Rate: 64 }
    ];
    myTeam.Players = myList;
    this.match.Team = myTeam;

    //create sets.  These will be created by server
    this.match.Set1 = <MatchSet>{ Id: 1, SetNumber: 1 };
    this.match.Set2 = <MatchSet>{ Id: 2, SetNumber: 2 };
    this.match.Set3 = <MatchSet>{ Id: 3, SetNumber: 3 };
    this.match.Set4 = <MatchSet>{ Id: 4, SetNumber: 4 };
    this.match.Set5 = <MatchSet>{ Id: 5, SetNumber: 5 };

    return this.match;
  };
 
  getMatch() {
    
    return this.match;
  }
}
