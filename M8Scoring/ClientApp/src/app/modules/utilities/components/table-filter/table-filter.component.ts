import { Component, OnInit, Input, Output, EventEmitter, ElementRef, AfterViewInit } from '@angular/core';
import { HttpClient, HttpUrlEncodingCodec } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { Observable, pipe } from 'rxjs';
import { filter, map, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { fromEvent } from 'rxjs/observable/fromEvent';

@Component({
  host: {
    '(document:click)': 'onOutsideClick($event)'
  },
  selector: 'table-filter',
  templateUrl: './table-filter.component.html',
  styleUrls: ['./table-filter.component.css']
})
export class TableFilterComponent implements OnInit, AfterViewInit {
  results: string[];
  activeIdx: number;
  filterText = '';
  searchBox: HTMLElement;
  typeahead$: Observable<{}>;

  constructor(private mEref: ElementRef, private http: HttpClient) {
    this.results = new Array<string>();
    this.activeIdx = -1;
  }
  @Input() placeholder: string;
  @Input() quickSearchUrl: string;
  @Output() filtered = new EventEmitter();

  ngOnInit() { }

  ngAfterViewInit() {
    this.searchBox = document.getElementById('search-box');
    this.typeahead$ = fromEvent(this.searchBox, 'input').pipe(
      map((e: KeyboardEvent) => e.target.value),
      filter(text => text.length > 2),
      filter(text => text.length < 5),
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap((text:string) => this.http.get(this.quickSearchUrl + '/' + encodeURIComponent(text)))
    );

    this.typeahead$.subscribe(data => {
      console.log(data);
      //Console.log("searching quicksearch..");
      let i = 1;
    }, err => console.log(err))
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

  }

  clearList() {
    this.results.length = 0;
    this.activeIdx = -1;
  }
}
