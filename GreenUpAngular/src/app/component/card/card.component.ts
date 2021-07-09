import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent implements OnInit {
  @Input() missionCard?: missionComp;

  constructor(
    private router: Router
  ) { }

  ngOnInit() {}

  public lookMission(e: any): void {
    console.log('voir la mission', e);
    this.router.navigate(['/missions']); //Ajouter id
  }
}

interface missionComp {
  title: string;
  subtitle: string;
  location: string;
  point: number;
  titleDesc: string;
  desc: string;
  date: string;
}
