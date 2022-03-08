import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { Subscription } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { DialogTextComponent } from 'src/app/component/dialog/dialog-text/dialog-text.component';

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

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private _snackBar: MatSnackBar,
    private router: Router,
    private aRoute: ActivatedRoute,
    private dialog: MatDialog
  ) {
    this.typeSignUp = this.aRoute.snapshot.params['id'];
    this.dateMax = new Date(Date.now());
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

  public openTermesAndCondition(): void {
    const dialogRef = this.dialog.open(DialogTextComponent);
    dialogRef.afterClosed().subscribe(result => {
    });
  }

  public signUp(): void {
    if (this.form.valid) {
      this.userService.signUp(this.form.value, this.typeSignUp).subscribe(
        (res: any) => {
          if (res.error) {
            this.openSnackBar(res.error);
          } else {
            this.openSnackBar('Vous êtes maintenant inscrit');
            this.login();
          }
        },
        (error: any) => {
          this.openSnackBar("Error : " + error);
        }
      );
    }
  }

  public openSnackBar(message: string) {
    this._snackBar.open(message, 'Ok');
  }

  public login(): void {
      this.router.navigate(['/auth', `${this.typeSignUp}`]);
  }

  private initForm(): void {
    this.form = this.fb.group({
      mail: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('',  [Validators.required]),
      firstName: new FormControl(null),
      lastName: new FormControl(null),
      name: new FormControl(null),
      adress: new FormControl(null),
      phoneNumber: new FormControl(null),
      city: new FormControl(null),
      zipCode: new FormControl(null),
      birthDate: new FormControl(null),
      photo: new FormControl(null),
      rnaNumber: new FormControl(null),
      siretNumber: new FormControl(null)
    });
  }
}
