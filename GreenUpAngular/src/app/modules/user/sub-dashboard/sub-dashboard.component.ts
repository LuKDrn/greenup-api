import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-sub-dashboard',
  templateUrl: './sub-dashboard.component.html',
  styleUrls: ['./sub-dashboard.component.scss']
})
export class SubDashboardComponent implements OnInit {
  @Input() set typeDashboard(value: number){
    this.titleTest = `${value}`;
  } 
  public titleTest!: string;

  constructor() { 
  }

  ngOnInit(): void {
  }

}
