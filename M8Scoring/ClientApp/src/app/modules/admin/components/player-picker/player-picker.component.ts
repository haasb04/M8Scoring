import { Component, OnInit, Output, ElementRef, AfterViewInit, Inject, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map, debounceTime, distinctUntilChanged, switchMap } from 'rxjs/operators';
import { fromEvent } from 'rxjs/observable/fromEvent';

@Component({
  host: {
    '(document:click)': 'onOutsideClick($event)'
  },
  selector: 'player-picker',
  templateUrl: './player-picker.component.html',
  styleUrls: ['./player-picker.component.css']
})
export class PlayerPickerComponent implements OnInit, AfterViewInit {
  results: Player[];
  activeIdx: number;
  searchText: string;
  searchBox: HTMLElement;
  typeahead$: Observable<PlayerList>;
  url: string;
  spfInputs: ListSpfInput;

  constructor(private mEref: ElementRef, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    this.url = this.baseUrl + "api/player/all"
    this.spfInputs = <ListSpfInput>{ PageIndex: 0, SortCol: "Number", PageSize: 25 };
    this.activeIdx = -1;
  }

  @Output() playerPicked:EventEmitter<Player> = new EventEmitter<Player>();

  ngOnInit() {
    this.results = new Array<Player>();
  }

  ngAfterViewInit() {
    this.searchBox = document.getElementById('search-box');
    this.typeahead$ = fromEvent(this.searchBox, 'input').pipe(
      map((e: any) => e.target.value),
      filter(text => text.length > 0),
      filter(text => text.length < 30),
      debounceTime(500),
      distinctUntilChanged(),
      switchMap((text: string) => {
        this.spfInputs.Filter = text;
        return this.http.get<PlayerList>(this.url + '?listSpfInput=' + encodeURIComponent(JSON.stringify(this.spfInputs)));
      })
    );

    this.typeahead$.subscribe(data => {
      this.results = data.Data;
    }, err => console.log(err))
  }

  onOutsideClick(event) {
    if(this.searchText && this.searchText.length > 0 && !this.mEref.nativeElement.contains(event.target)) {
      this.playerPicked.emit(null);
      this.clearControl();
    }
  }

  pickPlayer(idx:number) {
    this.playerPicked.emit(this.results[idx]);
    this.clearControl();
  }

  clearControl() {
    this.searchText = '';
    this.results.length = 0;
  }

  onKeyDown(event) {
    console.log("key press");
    if (event.key == "Escape") {
      this.playerPicked.emit(null);
      this.clearControl();
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
        this.pickPlayer(this.activeIdx);
        return;
      } else {
        this.playerPicked.emit(null);
        this.clearControl();
        return;
      }
    }
  }
}
