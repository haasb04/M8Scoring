import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MatchComponent } from './components/match/match.component';
import { NewMatchComponent } from './components/new-match/new-match.component';

const matchRoutes = [
  { path: 'match/:id', component: MatchComponent },
  { path: 'match/new/:teamId', component: NewMatchComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(matchRoutes)],
  exports: [RouterModule]
})
export class MatchRoutingModule { }
