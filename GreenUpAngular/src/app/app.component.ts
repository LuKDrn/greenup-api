import { Component, OnInit } from '@angular/core';
import * as feather from 'feather-icons';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'GreenUp';
  public isConnected: boolean = false;
  public loading: boolean = false;

  ngOnInit() {
    this.isConnected = this.checkJwt();
    feather.replace();
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
