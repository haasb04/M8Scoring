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
import { SortableColumnComponent } from './components/sortable-column/sortable-column.component';
import { SortService } from './services/sort.service';
import { SortableTableDirective } from './directives/sortable-table.directive';
import { TablePagingComponent } from './components/table-paging/table-paging.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TeamListComponent,
    SortableColumnComponent,
    SortableTableDirective,
    TablePagingComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch:'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'match', loadChildren: './modules/match/match.module#MatchModule'},
    ]),
    MatchModule
    
  ],
  providers: [SortService],
  bootstrap: [AppComponent]
})
export class AppModule { }
