import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-m8-import',
  templateUrl: './m8-import.component.html',
  styleUrls: ['./m8-import.component.css']
})
export class M8ImportComponent implements OnInit {
  
  constructor() { }

  ngOnInit() {
  }

  public uploadFinished = (event) => {
    console.log("Upload completed.");  
  }
}
