import { Component, OnInit } from '@angular/core';
import { MissionsService } from '../missions.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-missions',
  templateUrl: './missions.component.html',
  styleUrls: ['./missions.component.scss']
})
export class MissionsComponent implements OnInit {

  public nbMissions: number;
  public missions: Array<any>; //Changer le type en Mission

  constructor(
    private mserv: MissionsService,
    private router: Router,
  ) {
    this.missions = [];
    this.nbMissions = this.missions.length;
   }

  ngOnInit(): void {
    // this.getMissionsList();
  }

  public selectMission(id: number): void {
    this.router.navigate(['/mission/', id]);
  }

  public addMission(): void {
    this.router.navigate(['/mission/addMission']);
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
