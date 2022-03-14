import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-associations',
  templateUrl: './associations.component.html',
  styleUrls: ['./associations.component.scss']
})
export class AssociationsComponent implements OnInit {

  //preparer la liste des associations côté back et faire un appel côté front depuis le service

  constructor(
    private router: Router,
  ) { }

  ngOnInit(): void {
  }

  public selectAssociation(id: number): void {
    this.router.navigate(['/association/', id]);
  }
}
