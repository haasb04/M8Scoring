import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { MatchService } from '../../services/match.service';
import { onerror } from 'q';

@Component({
  selector: 'fifth-set-scenario',
  templateUrl: './fifth-set-scenario.component.html',
  styleUrls: ['./fifth-set-scenario.component.css']
})
export class FifthSetScenarioComponent implements AfterViewInit, OnDestroy {
  private fifthSets: FifthSetResults;
  private subscription;

  scores: number[] = new Array(15);
  mustHold: number[] = new Array(15);

  opponentScores: number[] = new Array(15);
  mustScore: number[] = new Array(15);

  constructor(private matchService: MatchService) { }

  ngAfterViewInit() {
    this.subscription = this.matchService.fifthSetResults.subscribe(result => {
      this.fifthSets = result;
      let idx: number;
      this.scores[0] = result.MustHold[0];
      this.mustHold[0] = result.MustHold[1];
      this.opponentScores[0] = result.MustGet[0];
      this.mustScore[0] = result.MustGet[1];

      for (idx = 1; idx < 16; idx++) {
        this.scores[idx] = this.scores[idx - 1] + 1;
        this.mustHold[idx] = result.MustHold[idx + 1];

        this.opponentScores[idx] = this.opponentScores[idx - 1] + 1;
        this.mustScore[idx] = result.MustGet[idx + 1];
      }
    }, error => console.log(error));
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
