import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { AssociationsService } from '../associations.service';

@Component({
  selector: 'app-association-auth',
  templateUrl: './association-auth.component.html',
  styleUrls: ['./association-auth.component.scss']
})
export class AssociationAuthComponent implements OnInit {
  public invalidLogin!: boolean;
  public form: FormGroup;
  public hide = true;

  constructor(
    private router: Router, 
    private http: HttpClient,
    private service: SharedService,
    private fb: FormBuilder,
    private associationService: AssociationsService
  ) { 
    this.form = this.fb.group({
      name: new FormControl('', [Validators.required]),
      siren: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required, Validators.required])
    });
  }

  ngOnInit(): void { }

  public getErrorMessage() {
    if (this.form.hasError('required')) {
      return 'You must enter a value';
    }
    return this.form.hasError('email') ? 'Les champs ne sont pas valides' : '';
  }

  public login(): void {
    this.associationService.login(this.form.value).subscribe(
      response => {
        const token = (<any>response).token;
        localStorage.setItem("jwt", token);
        this.invalidLogin = false;
        this.router.navigate(["/"]);
      }, err => {
        this.invalidLogin = true;
      });
  }
  public signUp(): void {
    this.router.navigate(['/associations/signUp']); 
  }
}
