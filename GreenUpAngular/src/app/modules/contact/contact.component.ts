import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  public form!: FormGroup;
  public loading: boolean;
  constructor(
    private fb: FormBuilder
  ) {
    this.initForm();
    this.loading = false;
  }

  ngOnInit(): void {
  }

  public envoyer(): void {
    console.log('envoyer le message');
  }

  private initForm(): void {
    this.form = this.fb.group({
      nom: new FormControl(''),
      mail: new FormControl(''),
      tel: new FormControl(''),
      message: new FormControl('')
    });
  }

}