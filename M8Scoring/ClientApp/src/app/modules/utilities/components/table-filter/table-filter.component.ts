import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'table-filter',
  templateUrl: './table-filter.component.html',
  styleUrls: ['./table-filter.component.css']
})
export class TableFilterComponent implements OnInit {
  filterText = '';

  constructor() { }
  @Input() placeholder: string;
  @Input() quickSearchUrl: string;
  @Output() filtered = new EventEmitter();

  ngOnInit() {
  }

  onFilter() {
    console.log("onFilter...");
  }
}
