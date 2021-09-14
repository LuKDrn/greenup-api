import { Component, OnInit } from '@angular/core';
import { MissionsService } from '../missions.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-missions',
  templateUrl: './missions.component.html',
  styleUrls: ['./missions.component.scss']
})
export class MissionsComponent implements OnInit {

  constructor(
    private mserv: MissionsService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    // this.getMissionsList();
  }

  public selectMission(id: number): void {
    this.router.navigate(['/mission/', id]);
  }

  private getMissionsList(): void {
    this.mserv.getAllMissions().subscribe(
      (res: any) => {
        console.log('res');
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }

}
