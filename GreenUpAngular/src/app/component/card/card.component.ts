import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent implements OnInit {
  @Input() image?: string;
  @Input() title?: string;
  @Input() location?: string;
  @Input() point?: string;
  @Input() titleDesc?: string; 
  @Input() desc?: string;
  @Input() date?: string;

  constructor(
    private router: Router
  ) {}

  ngOnInit() {}

}

// interface missionComp {
//   image?: string;
//   title?: string;
//   location?: string;
//   point?: string;
//   titleDesc?: string; 
//   desc?: string;
//   date?: string;
// }
