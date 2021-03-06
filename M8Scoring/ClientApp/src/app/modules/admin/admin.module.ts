import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { AdminComponent } from './components/admin/admin.component';
import { AdminRoutingModule } from './admin-routing.module';
import { AdminHomeComponent } from './components/admin-home/admin-home.component';
import { TeamListComponent } from './components/team-list/team-list.component';
import { UtilitiesModule } from '../utilities/utitlies.module';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { TeamComponent } from './components/team/team.component';
import { TeamEditComponent } from './components/team-edit/team-edit.component';
import { PlayerPickerComponent } from './components/player-picker/player-picker.component';
import { PlayerEditComponent } from './components/player-edit/player-edit.component';
import { PlayerComponent } from './components/player/player.component';
import { M8ImportComponent } from './components/m8-import/m8-import.component';


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    AdminRoutingModule,
    UtilitiesModule
  ],
  declarations: [
    AdminComponent,
    AdminHomeComponent,
    TeamListComponent,
    TeamComponent,
    TeamEditComponent,
    PlayerListComponent,
    PlayerComponent,
    PlayerPickerComponent,
    PlayerEditComponent,
    M8ImportComponent
  ]
})
export class AdminModule { }
