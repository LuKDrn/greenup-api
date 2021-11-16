import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Mission } from 'src/app/model/mission.model';
import { MissionsService } from '../missions.service';


@Component({
  selector: 'app-mission-id',
  templateUrl: './mission-id.component.html',
  styleUrls: ['./mission-id.component.scss']
})
export class MissionIdComponent implements OnInit {
public mission : Mission;
  constructor(
    private router:Router,
    private route: ActivatedRoute,
    private missionService:MissionsService
  ) {
    this.mission = new Mission();
   }


  ngOnInit(): void {
    const id = this.route.snapshot.params.id;
    this.getOneMission(id);
  }

  public missions(): void {
    this.router.navigate(['/missions']);
  }

  public getOneMission(id:number) : any {
    this.missionService.getOneMission(id).subscribe(
      (res: Mission) => {
        this.mission = res;
        console.log('res', this.mission);
        return this.mission;
      },
      (error: any) => {
        console.log('error', error);
        return error;
      }
    );
  }
}
