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
      Mail: new FormControl('', [Validators.required, Validators.email, Validators.email]),
      Password: new FormControl('', [Validators.required, Validators.required]),
      FirstName: new FormControl('', []),
      LastName: new FormControl('', []),
      address: new FormControl('', []),
      BirthDate: new FormControl('', []),
      point: new FormControl(0, []),
      // PhotoId: new FormControl(0, [])
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
    console.log(`s'inscrire`, this.form.value);
    this.userService.signUp(this.form.value).subscribe(
      (res: any) => {
        console.log('res');
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }
}
