import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  public comp!: HomeComp;

  constructor(
    public router: Router
  ) {}

  ngOnInit() {
    this.comp = {
      loading: true,
      missions: [
        {
          title: 'Mission1',
          subtitle: 'Ramassage de déchet',
          content: "Keep close to Nature's heart... and break clear away, once in awhile, and climb a mountain or spend a week in the woods. Wash your spirit clean."
        },
        {
          title: 'Mission2',
          subtitle: 'Collecte alimentaire',
          content: "Keep close to Nature's heart... and break clear away, once in awhile, and climb a mountain or spend a week in the woods. Wash your spirit clean."
        },
        {
          title: 'Mission3',
          subtitle: "Sensibilisation à l'environnement",
          content: "Keep close to Nature's heart... and break clear away, once in awhile, and climb a mountain or spend a week in the woods. Wash your spirit clean."
        }
      ]
    };
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

interface Mission {
  title: string; 
  subtitle: string; 
  content?: string;
}
