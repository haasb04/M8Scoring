import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AdminComponent } from './components/admin/admin.component';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { TeamListComponent } from './components/team-list/team-list.component';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { TeamEditComponent } from './components/team-edit/team-edit.component';
import { TeamComponent } from './components/team/team.component';
import { PlayerEditComponent } from './components/player-edit/player-edit.component';
import { PlayerComponent } from './components/player/player.component';
import { M8ImportComponent } from './components/m8-import/m8-import.component';

const routes = [
  {
    path: 'admin', component: AdminComponent,
    children: [
      { path: '', component: AdminHomeComponent },
      { path: 'teams', component: TeamListComponent },
      { path: 'team/create', component: TeamEditComponent },
      { path: 'team/edit/:id', component: TeamEditComponent },
      { path: 'team/:id', component: TeamComponent },
      { path: 'players', component: PlayerListComponent },
      { path: 'player/create', component: PlayerEditComponent },
      { path: 'player/edit/:id', component: PlayerEditComponent },
      { path: 'player/:id', component: PlayerComponent },
      { path: 'import', component: M8ImportComponent}
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule
{ }
