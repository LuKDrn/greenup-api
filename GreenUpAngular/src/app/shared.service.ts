import { Injectable } from '@angular/core';

import { HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {
  readonly APIUrl="https://localhost:5001/api";
  public isConnected: boolean;

  constructor(private http:HttpClient) {
    this.isConnected = false;
  }

  getUserList(): Observable<any[]>{
    return this.http.get<any>(this.APIUrl+'/user');
  }

  public deleteToken(): void {
    localStorage.removeItem('jwt');
  }

}
