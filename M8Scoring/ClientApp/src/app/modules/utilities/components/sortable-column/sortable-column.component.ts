import { Component, Input, OnInit, HostListener, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';

import { SortService } from '../../services/sort.service';

@Component({
  selector: '[sortable-column]',
  templateUrl: './sortable-column.component.html'
})
export class SortableColumnComponent implements OnInit, OnDestroy {

  constructor(private sortService: SortService) { }

  @Input('sortable-column')
  columnName: string;

  @Input('sort-direction')
  sortDirection: string = '';

  private columnSortedSubscription: Subscription;

  @HostListener('click')
  sort() {
    this.sortDirection = this.sortDirection == 'asc' ? 'desc' : 'asc';
    this.sortService.columnSorted({ sortColumn: this.columnName, sortDirection: this.sortDirection });
  }

  ngOnInit() {
    //subscripe to sort changes so we can react when other columns are sorted
    this.columnSortedSubscription = this.sortService.columnSorted$.subscribe(event => {
      //reset this column sort direction to hide the sort icon
      if (this.columnName != event.sortColumn) {
        this.sortDirection = '';
      }
    });
  }

  ngOnDestroy() {
    this.columnSortedSubscription.unsubscribe();
  }

}
