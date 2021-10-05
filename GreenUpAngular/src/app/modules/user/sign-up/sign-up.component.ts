import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.scss']
})
export class SignUpComponent implements OnInit {
  public form: FormGroup;
  public invalidLogin!: boolean;
  public hide = true;

  constructor(
    private fb: FormBuilder,
    private userService: UserService
  ) {

    this.form = this.fb.group({
      mail: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl('',  [Validators.required, Validators.minLength(6)]),
      firstName: new FormControl('', []),
      lastName: new FormControl('', []),
      adress: new FormControl('', []),
      city: new FormControl('', []),
      zipCode: new FormControl(0, []),
      birthDate: new FormControl('', []),
      photo: new FormControl(null, [])
    });
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
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }
}
