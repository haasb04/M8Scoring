import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { TablePagingComponent } from '../table-paging/table-paging.component';

@Component({
  selector: 'spf-base',
  templateUrl: './spf-base.component.html',
  styleUrls: ['./spf-base.component.css']
})
export class SpfBaseComponent implements OnInit {

  spfInputs: ListSpfInput;
  spfOutputs: ListSpfOutput;

  constructor(public router: Router, public route: ActivatedRoute) {
    this.spfInputs = <ListSpfInput>{};
    this.spfOutputs = <ListSpfOutput>{};
  }

  getData() {
    //expected to be overridden
  }

  ngOnInit() {
    this.route.paramMap.subscribe(
      param => TablePagingComponent.spfToMatrix(this.spfInputs, param));

    if (this.spfInputs.Filter == null) {
      this.spfInputs.Filter = "";
    }
      this.getData();
  }

  onPageChanged(page: PageChangedEvent) {
    this.spfInputs.PageIndex = page.newPage;
    this.getData();
    console.log("changed page: " + page.newPage);
  }

  onFiltered(filterText: string) {
    this.spfInputs.Filter = filterText;
    this.spfInputs.PageIndex = 0;
    this.getData();
  }

  onSorted(column: ColumnSortedEvent) {
    this.spfInputs.SortCol = column.sortColumn;
    this.spfInputs.SortOrder = (column.sortDirection == "asc" ? true : false);
    this.spfInputs.PageIndex = 0;
    this.getData();
  }
}
