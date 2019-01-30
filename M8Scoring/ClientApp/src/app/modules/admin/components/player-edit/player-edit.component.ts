import { Component, OnInit, Input, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from "@angular/common";

import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'player-edit',
  templateUrl: './player-edit.component.html',
  styleUrls: ['./player-edit.component.css']
})
export class PlayerEditComponent implements OnInit {
  title: string;
  player: Player;

  editMode: boolean;

  constructor(private location: Location, private activateRoute: ActivatedRoute,
    private router: Router,
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {

    this.player = <Player>{};

    var id = +this.activateRoute.snapshot.params["id"];
    if (id) {
      this.editMode = true;

      var url = this.baseUrl + "api/player/" + id;
      this.http.get<Player>(url).subscribe(
        res => {
          this.player = res;
          this.title = "Edit - Player " + this.player.Number;
        }, error => console.error(error));
    } else {
      this.editMode = false;
      this.title = "Create new Player";
    }
  }

  onSubmit(player: Player) {
    var url = this.baseUrl + "api/player";

    if (this.editMode) {
      this.http.post<Player>(url, player).subscribe(
        res => {
          var v = res;
          console.log("Player " + v.Id + " has been updated.");
          this.location.back();
        }, error => console.log(error));
    } else {
      this.http.put<Player>(url, player)
        .subscribe(res => {
          var q = res;
          console.log("Player " + q.Id + " has been created.");
          this.location.back();
        }, error => console.log(error));
    }
  }

  onBack() {
    this.location.back();
  }

  ngOnInit() {
  }



}
