import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  public information: string;

  constructor() {
    this.information = 'Protegeons notre maison, engageons-nous dans les petites actions quotidiennes'; 
  }

  ngOnInit(): void {
  }



}
