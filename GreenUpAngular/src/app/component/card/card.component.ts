import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MissionComp } from 'src/app/home/home.component';
@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent implements OnInit {
  // @Input() image: string = '';
  // @Input() title: string = '';
  // @Input() location: string = '';
  // @Input() point: string = '';
  // @Input() titleDesc: string = ''; 
  // @Input() desc: string = '';
  // @Input() date: string = '';

  @Input() mission?: MissionComp;

  constructor(
    private router: Router
  ) {
    // this.mission = {
    //   image: this.image,
    //   title: this.title,
    //   location: this.location,
    //   point: this.point,
    //   titleDesc: this.titleDesc, 
    //   desc: this.desc,
    //   date: this.date,
    // }
  }

  ngOnInit() {
    console.log(this.mission);
  }

}

interface missionComp {
  image?: string;
  title?: string;
  location?: string;
  point?: string;
  titleDesc?: string; 
  desc?: string;
  date?: string;
}
