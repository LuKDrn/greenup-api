import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-sub-dashboard',
  templateUrl: './sub-dashboard.component.html',
  styleUrls: ['./sub-dashboard.component.scss']
})
export class SubDashboardComponent implements OnInit {
  @Input() set typeDashboard(value: number){
    switch (value) {
      case 0: 
        this.title = 'Mes missions';
        break;
      case 1:
        this.title = 'Mon panier';
        break;
      case 2: 
        this.title = 'Mes favoris';
        break;
      case 3: 
        this.title = 'Mes paramètres';
        break;
    }
    this.selectedIndex = value;
  } 
  @Input() user: any;
  public title!: string;
  public selectedIndex: number = 0;
  public isModify = false;
  public form!: FormGroup;

  constructor(
    private fb: FormBuilder
  ) {

  }

  ngOnInit(): void {
    this.initForm(this.user);
  }

  public toggleModify(e: MatSlideToggleChange): void {
    this.isModify = !this.isModify;
  }

  private initForm(data?: any): void {
    console.log('data', data);
    this.form = this.fb.group({
      mail: new FormControl(data.mail),
      password: new FormControl(''),
      confirmPassword: new FormControl(''),
      firstName: new FormControl(data.firstName),
      lastName: new FormControl(data.lastName),
      place: new FormControl(data.adresses[0].place),
      phoneNumber: new FormControl(data.phoneNumber),
      city: new FormControl(data.adresses[0].city),
      zipCode: new FormControl(data.adresses[0].zipCode),
      birthdate: new FormControl(data.birthdate),
      photo: new FormControl(data.photo),
      rnaNumber: new FormControl(data.rnaNumber),
      siretNumber: new FormControl(data.siretNumber)
    });
  }
}
