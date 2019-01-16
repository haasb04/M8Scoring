import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'table-paging',
  templateUrl: './table-paging.component.html',
  styleUrls: ['./table-paging.component.css']
})
export class TablePagingComponent implements OnInit {
  pages: number[];
  constructor() {
    this.pages = [0, 1, 2, 3, 4, 5];
  }

  @Input() spfData: ListSpfOutput;
  @Output() pageChanged = new EventEmitter<PageChangedEvent>();

  ngOnInit() {

  }

  onPageChanged(newPage: number) {
   //this.pageChanged.emit({ oldPage: this.currentPage, newPage: newPage });
  
  }

  onPrevious() {
   
   // this.pageChanged(emit({ oldPage: this.spfData.PageIndex, newPage: this.spfData.PageIndex+1 }));
  }

  onNext() {

  }
}

export interface PageChangedEvent {
  oldPage: number;
  newPage: number;
}

export interface ListSpfInput {
  SortCol: string;
  SortOrder: boolean;
  Filter: string;
  PageIndex: number;
  PageSize: number;
}

export interface ListSpfOutput {
  PageSize: number;
  TotalCount: number;
  TotalPages: number;
  PageIndex: number;
  HasPreviousPage: boolean;
  HasNextPage: boolean;
}
