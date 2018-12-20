import { Component, OnInit, Inject } from '@angular/core';
import { MatchService } from '../../../../services/match.service';

@Component({
  selector: 'app-match',
  templateUrl: './match.component.html',
  styleUrls: ['./match.component.css']
})
export class MatchComponent implements OnInit {
  match: Match;
   
  constructor(private matchService: MatchService, @Inject('BASE_URL') private baseUrl: string) {
    //this.match = this.matchService.match;
       
  }

  ngOnInit() {
    
  }

}
