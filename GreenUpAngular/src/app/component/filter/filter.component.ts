import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-filter',
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {

  public distanceTab: Array<number>;
  public typeTab: Array<filterElement>;
  public dureeTab: Array<filterElement>; 

  public typeChoice: Array<filterElement>;
  public dureeChoice: Array<filterElement>;
  public distanceChoice: number;

  constructor() {
    this.distanceTab = [10, 20, 30, 40, 50];
    this.typeTab = [{name:'Type 1', selected: false}, {name:'Type 2', selected: false}, {name:'Type 3', selected: false}, {name:'Type 4', selected: false}];
    this.dureeTab = [{name: 'Heures', selected: false},{name:'Journée', selected: false}, {name: 'Plusieurs jours', selected:false}];
    this.dureeChoice = [];
    // this.distanceChoice = this.distanceTab[0];
    this.distanceChoice = 0;
    this.typeChoice = [];
  }

  ngOnInit(): void {
  }

  public selectedType(event: any, type: filterElement): void {
    if (event.checked) {
      type.selected = true;
      this.typeChoice.push(type);
    } else {
      type.selected = false;
      const $index = this.typeChoice.indexOf(type);
      this.typeChoice.splice($index, 1);
    }
  }

  public selectedDuree(event: any, duree: filterElement): void {
    if (event.checked) {
      duree.selected = true;
      this.dureeChoice.push(duree);
    } else {
      duree.selected = false;
      const $index = this.dureeChoice.indexOf(duree);
      this.dureeChoice.splice($index, 1);
    }
  }

  public removeFilter(): void {
    for (let duree of this.dureeChoice) {
      duree.selected = false;
    }
    this.dureeChoice = [];
    for (let type of this.typeChoice) {
      type.selected = false;
    }
    this.typeChoice = [];
    this.distanceChoice = 0;
  }
}

interface filterElement {
  name: string;
  selected: boolean;
}
