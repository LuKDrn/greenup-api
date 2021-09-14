import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
})
export class ToolbarComponent implements OnInit {
  public title: string = 'GreenUp';

  constructor(
    private router: Router
  ) { }

  ngOnInit() {}

  public home(): void {
    this.router.navigate(['/home']);
  }

  public login(): void {
    this.router.navigate(['/auth']);
  }

}
