import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {

  public information: string;

  constructor() {
    this.information = 'Mention légale - GreenUp 2021'; 
  }

  ngOnInit(): void {
  }



}
