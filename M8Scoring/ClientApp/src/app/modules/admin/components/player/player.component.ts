import { Component, OnInit, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from "rxjs";
import { switchMap } from 'rxjs/operator/switchMap';
import { TablePagingComponent } from '../../../utilities/components/table-paging/table-paging.component';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {
  title: string;
  player: Player;
  url: string;

  spfInput: ListSpfInput = <ListSpfInput>{};

  constructor(private router: Router, private route: ActivatedRoute, private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
    var id = +this.route.snapshot.params["id"];
    if (id) {
      var url = this.baseUrl + "api/player/" + id;
      this.http.get<Player>(url).subscribe(
        result => {
          this.player = result;
          this.title = "Player - " + this.player.Number;
        }, error => console.error(error));
    } else {
      //error?
    }
  }

  ngOnInit() {
    this.player = <Player>{};

    this.route.paramMap.subscribe(
      param => TablePagingComponent.spfToMatrix(this.spfInput, param));
  }

  onEdit() {
    this.router.navigate(["admin/player/edit/" + this.player.Id, this.spfInput]);
  }

  onBack() {
    this.router.navigate(['./admin/players',this.spfInput]);
  }
}
