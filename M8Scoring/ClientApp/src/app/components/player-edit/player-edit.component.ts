import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'player-edit',
  templateUrl: './player-edit.component.html',
  styleUrls: ['./player-edit.component.css']
})
export class PlayerEditComponent implements OnInit {
  @Input() player: Player;

  constructor() { }


  ngOnInit() {
  }



}
