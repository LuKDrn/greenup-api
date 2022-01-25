import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
  public form!: FormGroup;
  public typeSignUp: string;
  public invalidLogin!: boolean;
  public hidePassword = true;
  public hideConfirmPassword = true;
  public conditionIsCheck: boolean;
  public dateMax: Date;
  public isUser: boolean;
  public isAssociation: boolean;
  public isCompany: boolean;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private _snackBar: MatSnackBar,
    private router: Router,
    private aRoute: ActivatedRoute
  ) {
    this.typeSignUp = this.aRoute.snapshot.params['id'];
    this.isUser = false;
    this.isAssociation = false;
    this.isCompany = false;
    this.dateMax = new Date(Date.now());
    this.checkType();
    this.initForm();
    this.conditionIsCheck = false;
   }

  ngOnInit(): void {}

  public getErrorMessage() {
    if (this.form.hasError('required')) {
      return 'You must enter a value';
    }

    return this.form.hasError('email') ? 'Les champs ne sont pas valides' : '';
  }

  public signUp(): void {
    console.log('form', this.form.value);
    if (this.form.valid) {
      // this.form.get('birthDate')?.setValue(new Date().toISOString());
      this.userService.signUp(this.form.value).subscribe(
        (res: any) => {
          console.log('res', res);
          if (res.error) {
            this.openSnackBar(res.error);
          } else {
            this.openSnackBar('Vous êtes maintenant inscrit');
            this.login();
          }
        },
        (error: any) => {
          console.log('error', error);
        }
      );
    }
  }

  public openSnackBar(message: string) {
    this._snackBar.open(message, 'Ok');
  }

  public login(): void {
    this.router.navigate(['/auth']);
  }

  private initForm(): void {
    this.form = this.fb.group({
      mail: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('',  [Validators.required]),
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      adress: new FormControl(''),
      PhoneNumber: new FormControl(''),
      city: new FormControl(''),
      zipCode: new FormControl(null),
      birthDate: new FormControl(''),
      photo: new FormControl(null),
      isUser: new FormControl(this.isUser),
      isAssociation: new FormControl(this.isAssociation),
      isCompany: new FormControl(this.isCompany),
      rnaNumber: new FormControl(null),
      siretNumber: new FormControl(null)
    });
  }

  private checkType(): void {
    if (this.typeSignUp === 'user') {
      this.isUser = true;
      this.isAssociation = false;
      this.isCompany = false;
    } else if (this.typeSignUp === 'association') {
      this.isUser = false;
      this.isAssociation = true;
      this.isCompany = false;
    } else {
      this.isUser = false;
      this.isAssociation = false;
      this.isCompany = true;
    }
  }
}
