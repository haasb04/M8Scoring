import { Component, Input, Output, ViewChild, AfterViewInit, EventEmitter, ElementRef } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { fromEvent } from 'rxjs/observable/fromEvent';
import { filter, map, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';


@Component({
  host: {
    '(document:click)':'onOutsideClick($event)'
  },
  selector: 'opponent-selector',
  templateUrl: './opponent-selector.component.html',
  styleUrls: ['./opponent-selector.component.css']
})
export class OpponentSelectorComponent implements AfterViewInit {
  results: OpponentSearch[];
  activeIdx: number;
  filterText = '';
  filterTextChange = new EventEmitter<string>();
  typeahead$: Observable<OpponentSearch[]>;

  constructor(private mEref: ElementRef,private http: HttpClient) {
    this.results = new Array<OpponentSearch>();
    this.activeIdx = -1;
  }

  @Input() placeholder: string;
  @Input() searchUrl: string;
  @Input() opponent: number;
  @Output() opponentChange = new EventEmitter<number>();
  @ViewChild('searchBox') searchBox;

  ngAfterViewInit() {
    this.typeahead$ = fromEvent(this.searchBox.nativeElement, 'input').pipe(
      map((e: any) => e.target.value),
      filter(text => text.length < 20),
      debounceTime(500),
      distinctUntilChanged(),
      switchMap((text:string) => this.http.get<OpponentSearch[]>(this.searchUrl + '/' + encodeURIComponent(text)))
    );

    this.typeahead$.subscribe(data => {
      this.clearList();
      this.results = data;
    }, err => console.log(err))
  }

  onKeyDown(event) {
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
        this.setFilter(this.activeIdx);
        return;
      } 
    }
  }

  setFilter(idx) {
    if (this.results.length > idx) {
      this.filterText = this.results[idx].Label;
      this.opponent = this.results[idx].TeamId;
      this.opponentChange.emit(this.results[idx].TeamId);
      this.clearList();
      this.searchBox.nativeElement.focus();
    }
  }

  clearList() {
    this.results.length = 0;
    this.activeIdx = -1;
  }

  onOutsideClick(event) {
    if (this.filterText && this.filterText.length > 0 && !this.mEref.nativeElement.contains(event.target)) {
      //this.filterText = '';
      this.clearList();
    }
  }
}
