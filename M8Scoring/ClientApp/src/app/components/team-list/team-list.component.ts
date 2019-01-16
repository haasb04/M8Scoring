import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { ColumnSortedEvent } from '../../services/sort.service';
import { PageChangedEvent, ListSpfInput, ListSpfOutput } from '../table-paging/table-paging.component';
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
  spfInputs: ListSpfInput;
  spfOutputs: ListSpfOutput;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.title = "Teams";
    this.url = baseUrl + "api/Team/all";
    //this.spfInputs = {};
    //this.spfInputs.Filter = "";
    //this.spfInputs.SortOrder = false;
    //this.spfInputs.SortCol = "Name";
    //this.spfInputs.PageSize = 10;
    //this.spfInputs.PageIndex = 1;

    this.spfInputs = { Filter: null, SortOrder: false, SortCol: "Name", PageSize: 10, PageIndex: 0 }; 
    this.getTeams();
  }

  onSelect(team: Team) {
    this.selectedTeam = team;
    console.log("team with Id " + this.selectedTeam.Id + " has been selected.");
  }

  onSorted(column: ColumnSortedEvent) {
    this.spfInputs.SortCol = column.sortColumn;
    this.spfInputs.SortOrder = column.sortDirection == "desc" ? false : true;
    this.getTeams();
    console.log("column sorted: " + column.sortColumn);
  }

  onPageChanged(page: PageChangedEvent) {
    this.spfInputs.PageIndex = page.newPage;
    this.getTeams();
    console.log("changed page: " + page.newPage);
  }

  getTeams() {
    this.http.get<TeamList>(this.url + '?listSpfInput=' + encodeURIComponent(JSON.stringify(this.spfInputs)))
      .subscribe(result => {
        this.teams = result.Data;
        this.spfOutputs = Object.assign({}, result.SpfOutput);

    }, error => console.error(error));
  }
}
