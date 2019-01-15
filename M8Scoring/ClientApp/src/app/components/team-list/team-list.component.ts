import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ColumnSortedEvent } from '../../services/sort.service';
import { PageChangedEvent, ListSpfInput } from '../table-paging/table-paging.component';
import { TeamList } from '../../interfaces/TeamList';

@Component({
  selector: 'team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.css']
})
export class TeamListComponent {
  title: string;
  selectedTeam: Team;
  teams: Team[];
  url: string;
  totalPages: 10; //get from server
  private spfInputs: ListSpfInput;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.title = "Teams";
    this.url = baseUrl + "api/Team/all";
    //this.spfInputs = {};
    //this.spfInputs.Filter = "";
    //this.spfInputs.SortOrder = false;
    //this.spfInputs.SortCol = "Name";
    //this.spfInputs.PageSize = 10;
    //this.spfInputs.PageIndex = 1;

    let  spf = new ListSpfInput() { Filter: "", SortOrder: false, SortCol: "Name", PageSize: 10, PageIndex: 1 };
    this.spfInputs = spf; 
    this.getTeams();
  }

  onSelect(team: Team) {
    this.selectedTeam = team;
    console.log("team with Id " + this.selectedTeam.Id + " has been selected.");
  }

  onSorted(column: ColumnSortedEvent) {
    this.spfInputs.SortCol = column.sortColumn;
    this.spfInputs.SortOrder = column.sortDirection == "asc" ? false : true;
    this.getTeams();
    console.log("column sorted: " + column.sortColumn);
  }

  onPageChanged(page: PageChangedEvent) {
    console.log("changed page: " + page.newPage);
  }

  getTeams() {
    this.http.get<TeamList>(this.url+'?listSpfInput=' + JSON.stringify(this.spfInputs)).subscribe(result => {
      this.teams = result.Data;

    }, error => console.error(error));
  }
}
