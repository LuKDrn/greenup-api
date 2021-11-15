import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
  public form: FormGroup;
  public invalidLogin!: boolean;
  public hidePassword = true;
  public hideConfirmPassword = true;
  public conditionIsCheck: boolean;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private _snackBar: MatSnackBar,
    private router: Router
  ) {

    this.form = this.fb.group({
      mail: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('',  [Validators.required]),
      firstName: new FormControl(''),
      lastName: new FormControl(''),
      adress: new FormControl(''),
      city: new FormControl(''),
      zipCode: new FormControl(0),
      birthDate: new FormControl(''),
      photo: new FormControl(null)
    });
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
    this.form.get('birthDate')?.setValue(new Date().toISOString());
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

  public openSnackBar(message: string) {
    this._snackBar.open(message, 'Ok');
  }

  public login(): void {
    this.router.navigate(['/auth']);
  }
}
