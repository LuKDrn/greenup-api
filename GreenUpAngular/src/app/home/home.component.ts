import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public comp: any;
  public slides: any = {

  };

  constructor(
    public router: Router
  ) {
    this.comp = {
      loading: false,
      missions: [
        {
          image:'src/assets/cardExemple1.png',
          title: 'Mission1',
          location: 'Angers (49000)',
          point: '50 points',
          titleDesc: 'Ramassage de déchet',
          desc: "Keep close to Nature's heart... and break clear away, once in awhile, and climb a mountain or spend a week in the woods. Wash your spirit clean.",
          date: '25/09/2022'
        },
        {
          image:'src/assets/cardExemple2.png',
          title: 'Mission2',
          location: 'Angers (49000)',
          point: '50 points',
          titleDesc: 'Collecte alimentaire',
          desc: "Keep close to Nature's heart... and break clear away, once in awhile, and climb a mountain or spend a week in the woods. Wash your spirit clean.",
          date: '25/09/2022'
        },
        {
          image:'src/assets/cardExemple3.png',
          title: 'Mission3',
          location: 'Angers (49000)',
          point: '50 points',
          titleDesc: "Sensibilisation à l'environnement",
          desc: "Keep close to Nature's heart... and break clear away, once in awhile, and climb a mountain or spend a week in the woods. Wash your spirit clean.",
          date: '25/09/2022'
        }
      ]
    };
  }

  ngOnInit() {
    console.log(this.comp);
  }

  public logOut(){
    localStorage.removeItem("jwt");
  }
}

interface HomeComp {
  loading: boolean;
  missions: Array<Mission>; 
}

export interface Mission {
  image?: string;
  title?: string;
  location?: string;
  point?: string;
  titleDesc?: string; 
  desc?: string;
  date?: string;
}
