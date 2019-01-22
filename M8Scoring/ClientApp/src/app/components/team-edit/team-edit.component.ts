import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'team-edit',
  templateUrl: './team-edit.component.html',
  styleUrls: ['./team-edit.component.css']
})
export class TeamEditComponent implements OnInit {
  title: string;
  team: Team;

  editMode: boolean;

  constructor(private activatedRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {

    //create an empty Team object from Team interface
    this.team = <Team>{};

    var id = +this.activatedRoute.snapshot.params["id"];
    if (id) {
      this.editMode = true;

      //fetch the quiz from the server
      var url = this.baseUrl + "api/team/" + id;
      this.http.get<Team>(url).subscribe(
        res => {
          this.team = res;
          this.title = "Edit - " + this.team.Name;
        }, error => console.error(error));
    } else {
      this.editMode = false;
      this.title = "Create new Team";
    }
  }

  onSubmit(team: Team) {
    var url = this.baseUrl + "api/team";

    if (this.editMode) {
      this.http.post<Team>(url, team)
        .subscribe(res => {
          var v = res;
          console.log("Team " + v.Id + " has been updated.");
          this.router.navigate(["home"]);
        }, error => console.log(error));
    } else {
      this.http.put<Team>(url, team)
        .subscribe(res => {
          var q = res;
          console.log("Team " + q.Id + " has been created.");
          this.router.navigate(["home"]);
        }, error => console.log(error));
    }
  }

  onBack() {
    this.router.navigate(["home"]);
  }

  ngOnInit() {
  }

}
