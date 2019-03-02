import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatchRoutingModule } from './match-routing.module';
import { MatchComponent } from './components/match/match.component';
import { MatchService } from './services/match.service';
import { PlayerSelectorComponent } from './components/player-selector/player-selector.component';
import { MatchSetComponent } from './components/match-set/match-set.component';
import { NewMatchComponent } from './components/new-match/new-match.component';
import { OpponentSelectorComponent } from './components/opponent-selector/opponent-selector.component';

@NgModule({
  imports: [
    CommonModule,
    MatchRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    MatchService
  ],
  declarations: [MatchComponent, MatchSetComponent, PlayerSelectorComponent, NewMatchComponent, OpponentSelectorComponent]
})
export class MatchModule { }
