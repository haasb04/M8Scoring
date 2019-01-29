import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { ParamMap } from '@angular/router';

@Component({
  selector: 'table-paging',
  templateUrl: './table-paging.component.html',
  styleUrls: ['./table-paging.component.css']
})
export class TablePagingComponent implements OnInit {
  constructor() {  }

  @Input() spfData: ListSpfOutput;
  @Output() pageChanged = new EventEmitter<PageChangedEvent>();

  ngOnInit() {

  }
  onFirst() {
    this.pageChanged.emit({ oldPage: this.spfData.PageIndex, newPage: 0 });
  }

  onLast() {
    this.pageChanged.emit({ oldPage: this.spfData.PageIndex, newPage: this.spfData.TotalPages - 1 });
  }

  onPrevious() {
    if (this.spfData.PageIndex - 1 >= 0) {
      this.pageChanged.emit({ oldPage: this.spfData.PageIndex, newPage: this.spfData.PageIndex - 1 });
    }
  }

  onNext() {
    if (this.spfData.PageIndex + 1 < this.spfData.TotalPages) {
      this.pageChanged.emit({ oldPage: this.spfData.PageIndex, newPage: this.spfData.PageIndex + 1 });
    }
  }

  static spfToMatrix(obj: ListSpfInput, param: ParamMap) {
      obj.Filter = param.get('Filter');
      obj.PageIndex = +param.get('PageIndex');
      obj.SortCol = param.get("SortCol");
      obj.SortOrder = (param.get("SortOrder") == "true")
  }
}




