import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MatchRoutingModule } from './match-routing.module';
import { MatchComponent } from './components/match/match.component';
import { MatchService } from '../../services/match.service';

@NgModule({
  imports: [
    CommonModule,
    MatchRoutingModule
  ],
  providers: [
    MatchService
  ],
  declarations: [MatchComponent]
})
export class MatchModule { }
