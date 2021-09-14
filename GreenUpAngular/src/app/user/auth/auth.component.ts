import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from '../../shared.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  public invalidLogin!: boolean;
  public form: FormGroup;
  public hide = true;

  constructor(
    private router: Router, 
    private http: HttpClient,
    private service: SharedService,
    private fb: FormBuilder
  ) { 
    this.form = this.fb.group({
      email: new FormControl('', [Validators.required, Validators.email, Validators.email]),
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

  public login(form: NgForm): void {
    const credentials = JSON.stringify(form.value);

    this.http.post("https://localhost:5001/auth/login", credentials, {
      headers: new HttpHeaders({
        "Content-Type": "application/json"
      })
    }).subscribe(response => {
      const token = (<any>response).token;
      localStorage.setItem("jwt", token);
      this.invalidLogin = false;
      this.router.navigate(["/"]);
    }, err => {
      this.invalidLogin = true;
    });
  }

  public signUp(): void {
    this.router.navigate(['/signUp']); 
  }

}