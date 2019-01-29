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

import { UtilitiesModule } from './modules/utilities/utitlies.module';
import { PlayerEditComponent } from './components/player-edit/player-edit.component';
import { PlayerComponent } from './components/player/player.component';
import { AdminModule } from './modules/Admin/admin.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PlayerEditComponent,
    PlayerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    MatchModule,
    AdminModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'home', component: HomeComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'player/create', component: PlayerEditComponent },
      { path: 'player/edit/:id', component: PlayerEditComponent },
      { path: 'player/:id', component: PlayerComponent }
    ]),
    
    UtilitiesModule
    
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
