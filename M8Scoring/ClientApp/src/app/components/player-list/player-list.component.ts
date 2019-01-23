import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {
  @Input() players: Player[];
  @Output() selectedPlayer = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  onSorted(column: ColumnSortedEvent) {
    switch (column.sortColumn) {
      case 'Number':
        this.players.sort((a, b): number => {
          if (a.Number > b.Number) return column.sortDirection == "asc" ? 1 : -1;
          if (a.Number < b.Number) return column.sortDirection == "asc" ? -1 : 1;
          return 0;
        });
        break;
      case 'FirstName':
        this.players.sort((a, b): number => {
          if (a.FirstName < b.FirstName) return column.sortDirection == "asc"?1:-1;
          if (a.FirstName > b.FirstName) return column.sortDirection == "asc" ? -1 : 1;
          return 0;
        });
        break;
      case 'LastName':
        this.players.sort((a, b): number => {
          if (a.LastName > b.LastName) return column.sortDirection == "asc" ? 1 : -1;
          if (a.LastName < b.LastName) return column.sortDirection == "asc" ? -1 : 1;
          return 0;
        });
        break;
    }
  }

  onSelect(player: Player) {
    this.selectedPlayer.emit(player);
  }
}
