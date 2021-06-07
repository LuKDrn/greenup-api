import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MenuController } from '@ionic/angular';
import { MenuComponent } from '../menu/menu.component';
@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss']
})
export class HomePage extends MenuComponent implements OnInit {
  public comp!: HomeComp;

  constructor(
    public menu: MenuController,
    public router: Router
  ) {
    super(menu);
  }

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
  }

  public login(): void {
    void this.router.navigate(['/auth']);
  }

  public logOut(){
    localStorage.removeItem("jwt");
  }

  public openMenu(): void {
    this.openFirst();
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