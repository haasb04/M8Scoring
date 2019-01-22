import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { TeamList } from '../../interfaces/TeamList';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

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

  spfInputs: ListSpfInput;
  spfOutputs: ListSpfOutput;

  //time = new Observable(observer => { setInterval(() => observer.next(new Date().toString()), 1000 )});

  constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.title = "Teams";
    this.url = baseUrl + "api/Team/";

    //Define Sort/Page/Filter variables, used with table-filter and table-paging components
    this.spfInputs = { Filter: null, SortOrder: false, SortCol: "Name", PageSize: 10, PageIndex: 0 };
    this.spfOutputs = { HasNextPage: false, HasPreviousPage: false, PageIndex: 1, PageSize: 10, TotalCount: 0, TotalPages: 0, Filtered: false };
    this.getTeams();
  }

  onSelect(team: Team) {
    this.selectedTeam = team;
    console.log("team with Id " + this.selectedTeam.Id + " has been selected.");
    this.router.navigate(["team/" + this.selectedTeam.Id]);
  }
   
  getTeams() {
    this.http.get<TeamList>(this.url + 'all?listSpfInput=' + encodeURIComponent(JSON.stringify(this.spfInputs)))
      .subscribe(result => {
        this.teams = result.Data;
        this.spfOutputs = result.SpfOutput;

    }, error => console.error(error));
  }


  //sortable-table handlers
  onSorted(column: ColumnSortedEvent) {
    this.spfInputs.PageIndex = 0;
    this.spfInputs.SortCol = column.sortColumn;
    this.spfInputs.SortOrder = column.sortDirection == "desc" ? false : true;
    this.getTeams();
    console.log("column sorted: " + column.sortColumn);
  }

  /// 

  //table-paging handlers
  onPageChanged(page: PageChangedEvent) {
      this.spfInputs.PageIndex = page.newPage;
      this.getTeams();
      console.log("changed page: " + page.newPage);
  }

  ///

  //table-filter handlers
  onFiltered(filterText: string) {
    this.spfInputs.Filter = filterText;
    this.spfInputs.PageIndex = 0;
    this.getTeams();
  }
  ///


}
