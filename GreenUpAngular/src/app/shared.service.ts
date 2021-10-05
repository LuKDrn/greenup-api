import { Injectable } from '@angular/core';

import { HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import jwt_decode from 'jwt-decode';
import { UserService } from './modules/user/user.service';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
readonly APIUrl="https://localhost:5001/api"
  public jwt: string | any;
  public isConnected: boolean;
  constructor(private http:HttpClient, private userService: UserService) 
  {
    this.isConnected = false;
    if (localStorage.getItem('jwt')) {
      this.jwt = localStorage.getItem('jwt');
    }
  }

  getUserList(): Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/user');
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
    const $jwt: any = localStorage.getItem('jwt');
    const $userToken = this.getDecodedAccessToken(this.jwt);
    console.log('$jwt',$userToken.id);
    this.userService.getUser($userToken.id).subscribe(
      (res: any) => {
        this.isConnected = true;
        return res;
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }

  public deleteToken(): any {
    localStorage.setItem('jwt', '');
  }
}
