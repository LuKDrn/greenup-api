import { Component, OnChanges, OnInit } from '@angular/core';
import * as feather from 'feather-icons';
import { UserService } from './modules/user/user.service';
import { SharedService } from './shared.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnChanges {
  title = 'GreenUp';
  public isConnected: boolean = false;
  public loading: boolean = false;

  constructor(
    public sharedService: SharedService,
    private us: UserService,
  ) {}

  ngOnInit() {
    this.isConnected = this.us.isConnected;
    feather.replace();
  }

  ngOnChanges(): void {
    this.isConnected = this.us.checkJwt();
  }

  public checkJwt(): boolean {
    this.loading = true;
    const $jwt = localStorage.getItem('jwt');
    if ($jwt) {
      this.loading = false;
      return true;
    } else {
      this.loading = false;
      return false;
    }
  }
}
