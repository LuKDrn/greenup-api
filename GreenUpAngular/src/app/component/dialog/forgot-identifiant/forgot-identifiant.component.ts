import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-forgot-identifiant',
  templateUrl: './forgot-identifiant.component.html',
  styleUrls: ['./forgot-identifiant.component.scss']
})
export class ForgotIdentifiantComponent implements OnInit {
  public form!: FormGroup;

  constructor(
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
  }

  private initForm(): void {
    this.form = this.fb.group({
      lastName: new FormControl(''),
      firstName: new FormControl(''),
      email: new FormControl('')
    })
  }

}
