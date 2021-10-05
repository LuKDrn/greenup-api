import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../user.service';
import * as jwt_decode from "jwt-decode";

@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.scss']
})
export class EditProfileComponent implements OnInit {
  public invalidLogin!: boolean;
  public form: FormGroup;
  public jwt: string | any;

  constructor(
    private router: Router,
    private http: HttpClient,
    private fb: FormBuilder,
    private userService: UserService
  ) {
    if (localStorage.getItem('jwt')) {
      this.jwt = localStorage.getItem('jwt');
    }
    this.initForm();
  }

  ngOnInit(): void {
    console.log('jwt', this.jwt);
  }

  public editProfile(): void {
    this.userService.edit(this.form.value).subscribe(
      (res: any) => {
        console.log('res', res);
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }

  public deleteProfile(): void {
    const id: number = 0;
    this.userService.deleteProfile(id).subscribe(
      (res: any) => {
        console.log('res', res);
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }

  private initForm(): void {
    this.form = this.fb.group({
      // password: new FormControl('', [Validators.minLength(6)]),
      // confirmPassword: new FormControl('',  [Validators.minLength(6)]),
      firstName: new FormControl('', []),
      lastName: new FormControl('', []),
      adress: new FormControl('', []),
      city: new FormControl('', []),
      zipCode: new FormControl(0, []),
      birthDate: new FormControl('', []),
      // photo: new FormControl(null, [])
    });
  }

  private initUser(): void {
    // this.
  }
}
