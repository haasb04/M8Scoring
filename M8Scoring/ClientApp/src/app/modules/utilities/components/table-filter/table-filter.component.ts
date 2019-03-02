import { Component, Input, Output, EventEmitter, ElementRef, AfterViewInit, OnInit, ViewChild } from '@angular/core';
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
export class TableFilterComponent implements AfterViewInit, OnInit{
  results: string[];
  activeIdx: number;
  filterText = '';
  filterApplied: boolean;
  typeahead$: Observable<string[]>;

  constructor(private mEref: ElementRef, private http: HttpClient) {
    this.results = new Array<string>();
    this.activeIdx = -1;
  }
  @Input() placeholder: string;
  @Input() quickSearchUrl: string;
  @Input() initialFilterText: string;

  @Output() filtered = new EventEmitter();
  @ViewChild('searchBox') searchBox;

  ngOnInit() {
    this.filterText = this.initialFilterText;
    this.filterApplied = this.initialFilterText.length > 0;
  }

  ngAfterViewInit() {
    this.typeahead$ = fromEvent(this.searchBox.nativeElement , 'input').pipe(
      map((e: any) => e.target.value),
      filter(text => text.length > 2),
      filter(text => text.length < 20),
      debounceTime(1000),
      distinctUntilChanged(),
      switchMap((text:string) => this.http.get<string[]>(this.quickSearchUrl + '/' + encodeURIComponent(text)))
    );

    this.typeahead$.subscribe(data => {
      if (!this.filterApplied) {
        this.clearList();
        this.results = data;
      }
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
      this.searchBox.focus();
    }
  }

  clearFilter() {
    this.filterText = "";
    this.filterApplied = false;
    this.filtered.emit(this.filterText);
  }

  onFilter() {
    this.filtered.emit(this.filterText);
    this.filterApplied = true;
    this.clearList();
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
      if (this.activeIdx >= 0 && this.results.length > this.activeIdx) {
        this.filterText = this.results[this.activeIdx];
        this.clearList();
        return;
      } else {
        this.onFilter();
        return;
      }
    }
    this.filterApplied = false;
  }

  clearList() {
    this.results.length = 0;
    this.activeIdx = -1;
  }
}
