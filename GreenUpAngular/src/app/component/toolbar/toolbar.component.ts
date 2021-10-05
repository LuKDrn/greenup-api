import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/modules/user/user.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
})
export class ToolbarComponent implements OnInit {
  public title: string = 'GreenUp';
  // public user?: User;

  constructor(
    private router: Router,
    private userService: UserService
  ) { 
  }

  ngOnInit() {

  }

  public home(): void {
    this.router.navigate(['/home']);
  }

  public login(): void {
    this.router.navigate(['/auth']);
  }

  public missions(): void {
    this.router.navigate(['/missions']);
  }

  public produits(): void {
    this.router.navigate(['/produits']);
  }

  public challenges(): void {
    this.router.navigate(['/challenges']);
  }

  public associations(): void {
    this.router.navigate(['/associations']);
  }

  public initUser(): void {
    // this.userService.
  }
}
