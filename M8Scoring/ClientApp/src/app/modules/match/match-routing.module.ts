import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { MatchComponent } from './components/match/match.component';

const adminRoutes = [
  {path: 'match', component: MatchComponent}
];

@NgModule({
  imports: [
    RouterModule.forChild(adminRoutes)],
  exports: [RouterModule]
})
export class MatchRoutingModule { }
