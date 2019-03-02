import { Component, OnInit, Inject, Input } from '@angular/core';
import { SpfBaseComponent } from '../../modules/utilities/components/spf-base/spf-base.component';
import { Router, ActivatedRoute } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'dashboard-team',
  templateUrl: './dashboard-team.component.html',
  styleUrls: ['./dashboard-team.component.css']
})
export class DashboardTeamComponent extends SpfBaseComponent {
  url: string;
  matches: MatchSummary[];
  selectedMatch: MatchSummary;

  constructor(public router: Router, public route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(router, route);

    this.url = baseUrl + "api/Home/"
  }
  @Input() team: Team;

  getData() {
    this.http.get<MatchSummaryList>(this.url + 'matches/' + this.team.Id + '?listSpfInput=' + encodeURIComponent(JSON.stringify(this.spfInputs)))
      .subscribe(res => {
        this.matches = res.Data;
        this.spfOutputs = res.SpfOutput;
      }, error => console.error(error));
  }

  onSelect(match: MatchSummary) {
    this.selectedMatch = match;
    console.log("Match " + this.selectedMatch.Id + " has been selected");


    this.router.navigate(["match/" + this.selectedMatch.Id]);
  }

  onAddMatch() {
    this.router.navigate(["match/new/" + this.team.Id]);
  }
}
