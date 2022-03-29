import { Injectable } from '@angular/core';

import { HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { environment } from 'src/environments/environment';

const baseUrl = `${environment.apiURL}`;


@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly APIUrl=`${baseUrl}/api`;
  public isConnected: boolean;
  public isLoading: boolean;

  constructor(private http:HttpClient) {
    this.isConnected = false;
    this.isLoading = false;
  }

  getUserList(): Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/user');
  }

  public deleteToken(): void {
    localStorage.removeItem('jwt');
  }

}
