import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/modules/user/user.service';
import jwt_decode from 'jwt-decode';
import { SharedService } from 'src/app/shared.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
})
export class ToolbarComponent implements OnInit {
  public title: string = 'GreenUp';
  public user: any; //User;
  public jwt: string | any;
  public isConnected: boolean

  constructor(
    private router: Router,
    private userService: UserService,
    public sharedService: SharedService
  ) { 
    if (localStorage.getItem('jwt')) {
      this.isConnected = true;
      this.user = this.sharedService.getProfile();
    } else {
      this.isConnected = false;
    }
  }

  ngOnInit() {}

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

  public logout(): void {
    this.sharedService.deleteToken();
    this.sharedService.isConnected = false;
    this.router.navigate(['/home']);
  }
}
