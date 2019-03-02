import { Component, Inject } from '@angular/core';

import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-new-match',
  templateUrl: './new-match.component.html',
  styleUrls: ['./new-match.component.css']
})
export class NewMatchComponent {
  title: string;
  team: Team;
  matchHeader: MatchHeader;

  editMode: boolean;
   
  constructor(private activateRoute: ActivatedRoute, private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.matchHeader = <MatchHeader>{};

    var id = +this.activateRoute.snapshot.params["teamId"];
    var url = this.baseUrl + "api/team/" + id;
    this.http.get<Team>(url).subscribe(
      res => {
        this.team = res;
      }
    );

    this.editMode = false; //wait to figure out how and what to edit on a created match.
    this.title = "Create a new Match";
    this.matchHeader.TeamId = id;
  }

  onSubmit(matchHeader: MatchHeader) {
    var url = this.baseUrl + "api/match";
    matchHeader.OpponentId = 35;
    this.http.put<MatchHeader>(url, matchHeader).subscribe(res => {
      var q = res;
      console.log("Match " + q.Id + " has been created.");
      this.router.navigate(["match/" + q.Id]);
    });
  }
}
