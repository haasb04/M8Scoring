import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  teams: Team[];

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    //fetch user team list from server
    var url = this.baseUrl + "api/home/all";
    this.http.get<Team[]>(url).subscribe(
      res => {
        this.teams = res;
      }, error => console.error(error));
  }
}
