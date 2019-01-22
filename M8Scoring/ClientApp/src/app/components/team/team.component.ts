import { Component, OnInit, Input, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit {
  title: string;
  team: Team;
  url: string;

  constructor(private activatedRoute: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router:Router) {

    var id = +this.activatedRoute.snapshot.params["id"];
    if (id) {
      //fetch the Team
      var url = this.baseUrl + "api/team/" + id;
      this.http.get<Team>(url).subscribe(
        res => {
          this.team = res;
          this.title = "Team - " + this.team.Name;
        }, error => console.error(error));
    } else {
      //error?
    }
  }

  ngOnInit() {
  }

  onEdit() {
    this.router.navigate(["team/edit", this.team.Id]);
  }

  onDelete() {
    if (confirm("Do you really want to delete this team?")) {
      var url = this.baseUrl + "api/team/" + this.team.Id;
      this.http.delete(url).subscribe(res => {
        console.log("Team " + this.team.Id + "has been deleted.");
        this.router.navigate(["home"]);
      }, error => console.log(error));
    }
  }
}
