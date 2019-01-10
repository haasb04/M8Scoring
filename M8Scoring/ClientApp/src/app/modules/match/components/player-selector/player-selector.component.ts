import { Component, OnInit, Input, Output} from '@angular/core';

@Component({
  selector: 'app-player-selector',
  templateUrl: './player-selector.component.html',
  styleUrls: ['./player-selector.component.css']
})
export class PlayerSelectorComponent implements OnInit {
  @Input() team: Team;
  @Input() matchSetPlayer: MatchSetPlayer;
  showPlayerSelector: boolean;

  constructor() {
    this.showPlayerSelector = false;
  }

  ngOnInit() {
  }

  psShowPlayers() {
    this.showPlayerSelector = !this.showPlayerSelector;
  }

  onSelectPlayer(player: Player) {
    this.matchSetPlayer.Player = player;
    this.matchSetPlayer.Rate = player.Rate;
    this.showPlayerSelector = false;
  }

  onMouseLeave() {
    this.showPlayerSelector = false;
  }
}
