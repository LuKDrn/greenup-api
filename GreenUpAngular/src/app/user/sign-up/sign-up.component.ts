import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  public form: FormGroup;
  public invalidLogin!: boolean;



  constructor(
    private fb: FormBuilder
  ) {

    this.form = this.fb.group({
      email: new FormControl('', [Validators.required, Validators.email, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.required]),
      firstName: new FormControl('', []),
      lastName: new FormControl('', []),
      address: new FormControl('', []),
      birthDate: new FormControl('', []),
      point: new FormControl(0, [])
    });
   }

  ngOnInit(): void {
  }

  public getErrorMessage() {
    if (this.form.hasError('required')) {
      return 'You must enter a value';
    }

    return this.form.hasError('email') ? 'Les champs ne sont pas valides' : '';
  }

  public signUp(): void {
    console.log(`s'inscrire`, this.form);
  }

}
