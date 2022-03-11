import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements OnInit {

  public information: string;

  constructor(
    private route: Router
  ) {
    this.information = 'Protegeons notre maison, engageons-nous dans les petites actions quotidiennes'; 
  }

  ngOnInit(): void {
  }

  public goToAPropos(): void {
    this.route.navigate(['/a-propos']);
  }

  public goToAccueil(): void {
    this.route.navigate(['/']);
  }


}
