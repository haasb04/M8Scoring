import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SortableColumnComponent } from './components/sortable-column/sortable-column.component';
import { SortableTableDirective } from './directives/sortable-table.directive';
import { TablePagingComponent } from './components/table-paging/table-paging.component';
import { SortService } from './services/sort.service';
import { TableFilterComponent } from './components/table-filter/table-filter.component';
import { FormsModule } from '@angular/forms';
import { SpfBaseComponent } from './components/spf-base/spf-base.component';

@NgModule({
  declarations: [
    SortableColumnComponent,
    SortableTableDirective,
    TablePagingComponent,
    SortableTableDirective,
    TableFilterComponent,
    SpfBaseComponent
  ],
  imports: [
    CommonModule, FormsModule
  ],
  exports: [
    TablePagingComponent,
    SortableColumnComponent,
    SortableTableDirective,
    TableFilterComponent 
  ],
  providers:[SortService]
})
export class UtilitiesModule { }
