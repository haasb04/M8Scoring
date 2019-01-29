import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable } from "rxjs";
import { switchMap } from 'rxjs/operator/switchMap';
import { TablePagingComponent } from '../../modules/utilities/components/table-paging/table-paging.component';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {
  spfInput: ListSpfInput = <ListSpfInput>{};

  constructor(private router: Router, private route:ActivatedRoute) { }

  ngOnInit() {
    
    this.route.paramMap.subscribe(
      param => TablePagingComponent.spfToMatrix(this.spfInput, param));
  }

  onBack() {
    this.router.navigate(['./admin/players',this.spfInput]);
  }
}
