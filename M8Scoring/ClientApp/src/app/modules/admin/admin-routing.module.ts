import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AdminComponent } from './components/admin/admin.component';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { TeamListComponent } from './components/team-list/team-list.component';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { TeamEditComponent } from './components/team-edit/team-edit.component';
import { TeamComponent } from './components/team/team.component';

const routes = [
  {
    path: 'admin', component: AdminComponent,
    children: [
      { path: '', component: AdminHomeComponent },
      { path: 'teams', component: TeamListComponent },
      { path: 'team/create', component: TeamEditComponent },
      { path: 'team/edit/:id', component: TeamEditComponent },
      { path: 'team/:id', component: TeamComponent },
      { path: 'players', component: PlayerListComponent}
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
