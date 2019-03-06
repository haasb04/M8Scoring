import { Component, AfterViewInit } from '@angular/core';
import { MatchService } from '../../services/match.service';
import { Location } from "@angular/common";

@Component({
  selector: 'app-report-view',
  templateUrl: './report-view.component.html',
  styleUrls: ['./report-view.component.css']
})
export class ReportViewComponent implements AfterViewInit{
  private match: Match;

  constructor(private location:Location, private matchService: MatchService) {
    this.match = this.matchService.match;
    if (this.match != null) {
      this.matchService.calculate(this.match);
    }
  }

  ngAfterViewInit() {
    

  }

  onBack() {
    this.location.back();
  }
}
