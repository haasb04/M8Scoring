import { Component, OnInit, Inject } from '@angular/core';
import { Location } from "@angular/common";
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { TablePagingComponent } from '../../../utilities/components/table-paging/table-paging.component';

@Component({
  selector: 'team-edit',
  templateUrl: './team-edit.component.html',
  styleUrls: ['./team-edit.component.css']
})
export class TeamEditComponent implements OnInit {
  title: string;
  team: Team;
  selectedPlayer: Player;
  spfInput: ListSpfInput;

  showPlayerPicker: boolean;
  editMode: boolean;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    private location: Location,
    @Inject('BASE_URL') private baseUrl: string) {

    //create an empty Team object from Team interface
    this.team = <Team>{Players: new Array<Player>() };
    this.spfInput = <ListSpfInput>{};

    var id = +this.route.snapshot.params["id"];
    if (id) {
      this.editMode = true;

      //fetch the team from the server
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

  onPlayerSelected(player: Player) {
    this.selectedPlayer = player;
  }

  editPlayer(player: Player) {
    this.router.navigate(["admin/player/edit/" + player.Id, this.spfInput]);
  }

  addPlayer() {
    this.showPlayerPicker = true;
  }

  onPlayerPicked(player: Player) {
    if (player != null && this.team.Players.indexOf(player) < 0) {
      this.team.Players.push(player);
    }
    this.showPlayerPicker = false;
  }

  removePlayer(player: Player) {
    var index: number = this.team.Players.indexOf(player);
    if (index > -1) {
      this.team.Players.splice(index, 1);
    }
  }

  onSubmit(team: Team) {
    var url = this.baseUrl + "api/team";

    if (this.editMode) {
      this.http.post<Team>(url, team)
        .subscribe(res => {
          var v = res;
          console.log("Team " + v.Id + " has been updated.");
          this.router.navigate(["admin/teams",this.spfInput]);
        }, error => console.log(error));
    } else {
      this.http.put<Team>(url, team)
        .subscribe(res => {
          var q = res;
          console.log("Team " + q.Id + " has been created.");
          this.router.navigate(["admin/teams", this.spfInput]);
        }, error => console.log(error));
    }
  }

  onBack() {
    this.location.back();
  }

  ngOnInit() {
    this.route.paramMap.subscribe(param => TablePagingComponent.spfToMatrix(this.spfInput, param));
  }
}
