import { Component, OnInit, Inject, SimpleChange, SimpleChanges } from '@angular/core';
import { MatchService } from '../../services/match.service';
import { Route, ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.css']
})
export class MatchComponent implements OnInit{
  match: Match;
  hidePanel: boolean;
 
  playerScore: number;
  opponentScore: number;

  constructor(private matchService: MatchService, @Inject('BASE_URL') private baseUrl: string, private activatedRoute: ActivatedRoute, public router:Router) {
    var matchId = +this.activatedRoute.snapshot.params["id"];
    this.matchService.getMatch(matchId).subscribe(
      data => {
        this.match = data;
      }
    );
    this.hidePanel = false;
  }

  ngOnInit() {

  }

  onSave() {
    this.matchService.saveMatch();
  }

  showReportView() {
    this.router.navigate(["match/reportView/"]);
  }
}
