import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-dialog-text',
  templateUrl: './dialog-text.component.html',
  styleUrls: ['./dialog-text.component.scss']
})
export class DialogTextComponent implements OnInit {
  public title: string;
  constructor() {
    this.title = 'Termes et conditions'
  }

  ngOnInit(): void {
  }

}
