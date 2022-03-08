import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public comp: any;
  public slides = [
    {'image': './../../assets/GarnierExemple.png'}, 
    {'image': './../../assets/GarnierExemple.png'},
    {'image': 'src/assets/GarnierExemple.png'}, 
    {'image': './../../assets/GarnierExemple.png'}
  ];
  public missions: MissionComp[] = [];//MissionComp[];

  constructor(
    public router: Router
  ) {}

  ngOnInit() {
  }

  
  public contact(): void {
    this.router.navigate(['/a-propos']);
  }
}

interface HomeComp {
  loading: boolean;
  missions: Array<MissionComp>; 
}

export interface MissionComp {
  image: string;
  title: string;
  location: string;
  point: string;
  titleDesc: string; 
  desc: string;
  date: string;
}

