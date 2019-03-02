import { Component, OnInit, Input } from '@angular/core';
import { MatchService } from '../../services/match.service';



@Component({
  selector: 'app-match-set',
  templateUrl: './match-set.component.html',
  styleUrls: ['./match-set.component.css']
})
export class MatchSetComponent implements OnInit {
  @Input() matchSet: MatchSet;

  constructor(private matchService: MatchService) {
  }

  ngOnInit() {
  
  }

  set player1Score(score: number) {
    this.matchSet.Player1.Score = score;
    this.matchService.calculateMatch();
  }
  get player1Score(): number { return this.matchSet.Player1.Score; }

  set player2Score(score: number) {
    this.matchSet.Player2.Score = score;
    this.matchService.calculateMatch();
  }
  get player2Score(): number { return this.matchSet.Player2.Score; }

  set player1Rate(rate: number) {
    this.matchSet.Player1.Rate = rate;
    this.matchService.calculateMatch();
  }
  get player1Rate(): number { return this.matchSet.Player1.Rate; }

  set player2Rate(rate: number) {
    this.matchSet.Player2.Rate = rate;
    this.matchService.calculateMatch();
  }
  get player2Rate(): number { return this.matchSet.Player2.Rate; }
}
