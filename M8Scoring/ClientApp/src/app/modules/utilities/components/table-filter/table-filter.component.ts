import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core';
import { HttpClient, HttpUrlEncodingCodec } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';

@Component({
  host: {
    '(document:click)': 'onOutsideClick($event)'
  },
  selector: 'table-filter',
  templateUrl: './table-filter.component.html',
  styleUrls: ['./table-filter.component.css']
})
export class TableFilterComponent implements OnInit {
  results: string[];
  activeIdx: number;
  filterText = '';

  constructor(private mEref: ElementRef, private http: HttpClient) {
    this.results = new Array<string>();
    this.activeIdx = -1;
  }
  @Input() placeholder: string;
  @Input() quickSearchUrl: string;
  @Output() filtered = new EventEmitter();

  ngOnInit() {
  }

  onOutsideClick(event) {
    if (!this.mEref.nativeElement.contains(event.target)) {
      this.clearList();
    }
  }

  setFilter(idx) {
    if (this.results.length > idx) {
      this.filterText = this.results[idx];
      this.clearList();
    }
  }

  onFilter() {
    console.log("onFilter...");
  }

  onKeyDown(event) {
    console.log("key press");
    if (event.key == "Escape") {
      this.clearList();
      return;
    } else if (event.key == "ArrowUp") {
      if (this.activeIdx > -1) {
        this.activeIdx--;
      }
      return;
    } else if (event.key == "ArrowDown") {
      if (this.activeIdx < this.results.length - 1) {
        this.activeIdx++;
      }
      return;
    } else if (event.key == "Enter") {
      if (this.activeIdx > 0 && this.results.length > this.activeIdx) {
        this.filterText = this.results[this.activeIdx];
        this.clearList();
        return;
      } else {
        this.onFilter();
        return;
      }
    }

    this.search();
    //this.results.push("test" + this.results.length + 1);
  }

  search() {
    //call server api (this.quickSearchUrl).  Should return an array of strings.
    //should only search after typing stops.  Then sets results into the results array.
  }

  clearList() {
    this.results.length = 0;
    this.activeIdx = -1;
  }
}
