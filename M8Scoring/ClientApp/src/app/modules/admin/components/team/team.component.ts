import { Component, OnInit, Input, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { TablePagingComponent } from '../../../utilities/components/table-paging/table-paging.component';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit {
  title: string;
  team: Team;
  url: string;

  //keep spfInputs for return to team list
  spfInput: ListSpfInput = <ListSpfInput>{};

  constructor(private route: ActivatedRoute, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string, private router:Router) {

    var id = +this.route.snapshot.params["id"];
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
    this.team = <Team>{};

    this.route.paramMap.subscribe(param => TablePagingComponent.spfToMatrix(this.spfInput, param));
  }

  onEdit() {
    this.router.navigate(["admin/team/edit/" + this.team.Id, this.spfInput]);
  }

  onBack() {
    this.router.navigate(['./admin/teams', this.spfInput])
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
