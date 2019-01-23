import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { MatchModule } from './modules/match/match.module';
import { TeamListComponent } from './components/team-list/team-list.component';

import { UtilitiesModule } from './modules/utilities/utitlies.module';
import { TeamEditComponent } from './components/team-edit/team-edit.component';
import { TeamComponent } from './components/team/team.component';
import { PlayerListComponent } from './components/player-list/player-list.component';
import { PlayerEditComponent } from './components/player-edit/player-edit.component';
import { PlayerComponent } from './components/player/player.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TeamListComponent,
    TeamEditComponent,
    TeamComponent,
    PlayerListComponent,
    PlayerEditComponent,
    PlayerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'match', loadChildren: './modules/match/match.module#MatchModule' },
      { path: 'team/create', component: TeamEditComponent },
      { path: 'team/edit/:id', component: TeamEditComponent },
      { path: 'team/:id', component: TeamComponent },
      { path: 'player/create', component: PlayerEditComponent },
      { path: 'player/edit/:id', component: PlayerEditComponent },
      { path: 'player/:id', component: PlayerComponent }
    ]),
    MatchModule,
    UtilitiesModule
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
