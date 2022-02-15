import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { SharedService } from 'src/app/shared.service';
import { UserService } from '../user.service';
import jwt_decode from 'jwt-decode';
import { User } from 'src/app/model/user.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  public invalidLogin!: boolean;
  public form: FormGroup;
  public jwt: string | any;
  public user: User;
  public loading: boolean;

  constructor(
    private router: Router,
    private http: HttpClient,
    private fb: FormBuilder,
    private userService: UserService,
    private sharedService: SharedService
  ) {
    this.loading = false;
    this.user = new User();
    this.getProfile();
    this.form = this.initForm();
  }

  ngOnInit(): void {}

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
    console.log('user', this.user);
    this.userService.deleteProfile(this.user.id).subscribe(
      (res: any) => {
        console.log('res', res);
        this.sharedService.deleteToken();
        this.router.navigate(['/home']);
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }

  private initForm(): FormGroup {
    return this.fb.group({
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

  private getDecodedAccessToken(token: string): any {
    try{
        return jwt_decode(token);
    }
    catch(Error){
        return null;
    }
  }

  public getProfile() : any {
    this.loading = true;
    const $jwt: any = localStorage.getItem('jwt');
    const $userToken = this.getDecodedAccessToken($jwt);
    this.userService.getUser($userToken.userId).subscribe(
      (res: User) => {
        this.user = res;
        console.log(this.user);
        this.sharedService.isConnected = true;
        this.loading = false;
        return res.id;
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }

  public logout(): void {
    localStorage.removeItem('jwt');
    this.router.navigate(['/home']);
  }
}
