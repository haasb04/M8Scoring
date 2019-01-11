import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.css']
})
export class TeamListComponent {
  title: string;
  selectedTeam: Team;
  teams: Team[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.title = "Teams";
    var url = baseUrl + "api/Team/all/100";
    http.get<Team[]>(url).subscribe(result => {
      this.teams = result;
    }, error => console.error(error));
  }

  onSelect(team: Team) {
    this.selectedTeam = team;
    console.log("team with Id " + this.selectedTeam.Id + " has been selected.");
  }
}
