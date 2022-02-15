import { Component, OnInit } from '@angular/core';
import { MissionsService } from '../missions.service';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { AddMissionComponent } from '../add-mission/add-mission.component';

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
    private dialog: MatDialog
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
    const dialogRef = this.dialog.open(AddMissionComponent, {
      width: '700px',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });

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
