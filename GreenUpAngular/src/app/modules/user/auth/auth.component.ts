import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { ForgotIdentifiantComponent } from 'src/app/component/dialog/forgot-identifiant/forgot-identifiant.component';
import { SharedService } from '../../../shared.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  public invalidLogin!: boolean;
  public form!: FormGroup;
  public typeLogin: string;
  public hide = true;

  constructor(
    private router: Router, 
    private aRoute: ActivatedRoute,
    private http: HttpClient,
    private service: SharedService,
    private fb: FormBuilder,
    private userService: UserService,
    private dialog: MatDialog
  ) {

    this.typeLogin = this.aRoute.snapshot.params['id'];
    console.log(this.typeLogin);
    this.initForm();
  }

  ngOnInit(): void { }

  public getErrorMessage() {
    if (this.form.hasError('required')) {
      return 'You must enter a value';
    }
    return this.form.hasError('email') ? 'Les champs ne sont pas valides' : '';
  }

  public onSubmit(): void { }

  public login(): void {
    this.userService.login(this.form.value).subscribe(
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
    this.router.navigate(['/signUp', `${this.typeLogin}`]); 
  }

  public forgotIdent(): void {
    const dialogRef = this.dialog.open(ForgotIdentifiantComponent);
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  private initForm(): void {
    this.form = this.fb.group({
      mail: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required])
    });
  }
}