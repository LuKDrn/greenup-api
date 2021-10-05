import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AssociationsService } from '../associations.service';

@Component({
  selector: 'app-association-sign-up',
  templateUrl: './association-sign-up.component.html',
  styleUrls: ['./association-sign-up.component.scss']
})
export class AssociationSignUpComponent implements OnInit {
  public form: FormGroup;
  public invalidLogin!: boolean;
  public hide = true;

  constructor(
    private fb: FormBuilder,
    private associationService: AssociationsService
  ) {

    this.form = this.fb.group({
      name: new FormControl('', [Validators.required]),
      siren: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      confirmPassword: new FormControl('',  [Validators.required, Validators.minLength(6)]),
      adress: new FormControl('', [Validators.required]),
      city: new FormControl('', [Validators.required]),
      zipCode: new FormControl(0, []),
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
    this.associationService.signUp(this.form.value).subscribe(
      (res: any) => {
        console.log('res', res);
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }
}
