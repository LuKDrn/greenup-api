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
  ) {
    const $missions = [
      {
        image:'./../../../assets/cardExemple1.png',
        title: 'Mission1',
        location: 'Angers (49000)',
        point: '50 points',
        titleDesc: 'Nettoyage des plages',
        desc: `Venir aider les associations à trier les déchets sur les plages d'Angers...`,
        date: '25/09/2022'
      },
      {
        image:'./../../../assets/cardExemple2.png',
        title: 'Mission2',
        location: 'Angers (49000)',
        point: '50 points',
        titleDesc: 'Nettoyage des plages',
        desc: `Venir aider les associations à trier les déchets sur les plages d'Angers...`,
        date: '25/09/2022'
      },
      {
        image:'./../../../assets/cardExemple3.png',
        title: 'Mission3',
        location: 'Angers (49000)',
        point: '50 points',
        titleDesc: 'Nettoyage des plages',
        desc: `Venir aider les associations à trier les déchets sur les plages d'Angers...`,
        date: '25/09/2022'
      }
    ];
    for (let $item of $missions) {
      this.missions.push($item);
    }
    // this.comp = {
    //   loading: false,
    // };
  }

  ngOnInit() {
    console.log(this.comp);
    console.log(this.missions);
  }

  public logOut(){
    localStorage.removeItem("jwt");
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
