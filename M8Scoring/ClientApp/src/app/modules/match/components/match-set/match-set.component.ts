import { Component, OnInit, Input } from '@angular/core';
import { MatchService } from '../../../../services/match.service';


@Component({
  selector: 'app-match-set',
  templateUrl: './match-set.component.html',
  styleUrls: ['./match-set.component.css']
})
export class MatchSetComponent implements OnInit {
  @Input() matchSet: MatchSet;
  constructor(private matchService: MatchService) { }

  ngOnInit() {
  
  }

  setPlayer() {
    //show team player selector
    this.matchSet.Player1 = <Player>this.matchService.match.Team.Players[2];
    this.matchSet.P1Rate = this.matchSet.Player1.Rate;
  }

  setOpponent() {
    //show opponent player selector
    this.matchSet.Player2 = <Player>this.matchService.match.Opponent.Players[3];
    this.matchSet.P2Rate = this.matchSet.Player2.Rate;

  }

}
