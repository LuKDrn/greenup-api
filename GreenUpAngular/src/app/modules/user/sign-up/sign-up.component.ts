import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../user.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

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
    private userService: UserService,
    private router: Router
  ) {
    this.form = this.initForm();
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
        if (!res.error) {
          this.router.navigate(['/auth']);
          console.log('res', res);
        }
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }

  public fileBrowseHandler(e: any) {
    if (e.files) {
      let me = this;
      let file = e.files[0];
      let reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = function () {
        console.log(reader.result);
        me.form.get('photo')?.setValue(reader.result);
      };
      reader.onerror = function (error) {
        console.log('Error: ', error);
        me.form.get('photo')?.setValue(null);
      };    
    } else {
      this.form.get('photo')?.setValue(null);
    }
  }

  private initForm(): FormGroup {
    return this.fb.group({
      mail: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl('',  [Validators.required, Validators.minLength(6)]),
      firstName: new FormControl('', []),
      lastName: new FormControl('', []),
      adress: new FormControl('', []),
      city: new FormControl('', []),
      zipCode: new FormControl(0, []),
      birthDate: new FormControl('', []),
      photo: new FormControl('', [])
    });
  }
}
