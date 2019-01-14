import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ColumnSortedEvent } from '../../services/sort.service';
import { PageChangedEvent } from '../table-paging/table-paging.component';

@Component({
  selector: 'team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.css']
})
export class TeamListComponent {
  title: string;
  selectedTeam: Team;
  teams: Team[];
  totalPages: 10; //get from server

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

  onSorted(column: ColumnSortedEvent) {
    console.log("column sorted: " + column.sortColumn);
  }

  onPageChanged(page: PageChangedEvent) {
    console.log("changed page: " + page.newPage);
  }
}
