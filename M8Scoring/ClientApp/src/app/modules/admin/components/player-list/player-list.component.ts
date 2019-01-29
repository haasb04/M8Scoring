import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router, ActivatedRoute } from '@angular/router';
import { SpfBaseComponent } from '../../../utilities/components/spf-base/spf-base.component';

@Component({
  selector: 'player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent extends SpfBaseComponent {
  title: string;
  selectedPlayer: Player;
  players: Player[];
  url: string;

  constructor(public router: Router, public route: ActivatedRoute,
    private http: HttpClient,
    @Inject('BASE_URL') baseUrl: string) {
    super(router, route);

    this.title = "Players";
    this.url = baseUrl + "api/Player/";
  }

  getData() {
    this.http.get<PlayerList>(this.url + 'all?listSpfInput=' + encodeURIComponent(JSON.stringify(this.spfInputs)))
      .subscribe(result => {
        this.players = result.Data;
        this.spfOutputs = result.SpfOutput;
      }, error => console.error(error));
  }

  //onSorted(column: ColumnSortedEvent) {


    //switch (column.sortColumn) {
    //  case 'Number':
    //    this.players.sort((a, b): number => {
    //      if (a.Number > b.Number) return column.sortDirection == "asc" ? 1 : -1;
    //      if (a.Number < b.Number) return column.sortDirection == "asc" ? -1 : 1;
    //      return 0;
    //    });
    //    break;
    //  case 'FirstName':
    //    this.players.sort((a, b): number => {
    //      if (a.FirstName < b.FirstName) return column.sortDirection == "asc" ? 1 : -1;
    //      if (a.FirstName > b.FirstName) return column.sortDirection == "asc" ? -1 : 1;
    //      return 0;
    //    });
    //    break;
    //  case 'LastName':
    //    this.players.sort((a, b): number => {
    //      if (a.LastName > b.LastName) return column.sortDirection == "asc" ? 1 : -1;
    //      if (a.LastName < b.LastName) return column.sortDirection == "asc" ? -1 : 1;
    //      return 0;
    //    });
    //    break;
    //}
  //}

  onSelect(player: Player) {
    this.selectedPlayer = player;
    console.log("player with Id " + this.selectedPlayer.Id + " has been selected.");
    this.router.navigate(["player/" + this.selectedPlayer.Id, this.spfInputs]);
  }
}
