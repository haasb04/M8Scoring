import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { SpfBaseComponent } from '../../../utilities/components/spf-base/spf-base.component';

@Component({
  selector: 'team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.css']
})
export class TeamListComponent extends SpfBaseComponent {
  title: string;
  selectedTeam: Team;
  teams: Team[];
  url: string;

  //time = new Observable(observer => { setInterval(() => observer.next(new Date().toString()), 1000 )});

  constructor(public router: Router, public route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    super(router, route);
    this.title = "Teams";
    this.url = baseUrl + "api/Team/";
  }

  onSelect(team: Team) {
    this.selectedTeam = team;
    console.log("team with Id " + this.selectedTeam.Id + " has been selected.");
    this.router.navigate(["admin/team/" + this.selectedTeam.Id, this.spfInputs]);
  }

  addNewTeam() {
    this.router.navigate(["admin/team/create", this.spfInputs]);
  }

  getData() {
    this.http.get<TeamList>(this.url + 'all?listSpfInput=' + encodeURIComponent(JSON.stringify(this.spfInputs)))
      .subscribe(result => {
        this.teams = result.Data;
        this.spfOutputs = result.SpfOutput;

      },
      error => console.error(error));
  }
}
