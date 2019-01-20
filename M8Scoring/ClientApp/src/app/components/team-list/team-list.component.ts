import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { TeamList } from '../../interfaces/TeamList';
import { Observable } from 'rxjs';

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
  time = new Observable(observer => {
    setInterval(() => observer.next(new Date().toString()), 1000)
  });
  
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.title = "Teams";
    this.url = baseUrl + "api/Team/";
    //this.spfInputs = {};
    //this.spfInputs.Filter = "";
    //this.spfInputs.SortOrder = false;
    //this.spfInputs.SortCol = "Name";
    //this.spfInputs.PageSize = 10;
    //this.spfInputs.PageIndex = 1;

    this.spfInputs = { Filter: null, SortOrder: false, SortCol: "Name", PageSize: 10, PageIndex: 0 };
    this.spfOutputs = { HasNextPage: false, HasPreviousPage: false, PageIndex: 1, PageSize: 10, TotalCount: 0, TotalPages: 0, Filtered: false };
    this.getTeams();
  }

  onSelect(team: Team) {
    this.selectedTeam = team;
    console.log("team with Id " + this.selectedTeam.Id + " has been selected.");
  }

  onSorted(column: ColumnSortedEvent) {
    this.spfInputs.PageIndex = 0;
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

  onFiltered(filterText: string) {
    this.spfInputs.Filter = filterText;
    this.spfInputs.PageIndex = 0;
    this.getTeams();
  }

  getTeams() {
    this.http.get<TeamList>(this.url + 'all?listSpfInput=' + encodeURIComponent(JSON.stringify(this.spfInputs)))
      .subscribe(result => {
        this.teams = result.Data;
        this.spfOutputs = result.SpfOutput;

    }, error => console.error(error));
  }
}
