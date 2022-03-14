import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Mission } from 'src/app/model/mission.model';
import { MissionComp } from 'src/app/modules/home/home.component';
@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent implements OnInit {
  @Input() mission?: any;

  constructor(
    private router: Router
  ) {}

  ngOnInit() {
    console.log(this.mission);
  }
}
