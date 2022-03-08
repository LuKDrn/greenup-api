import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  public headers = new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': "Bearer "+ localStorage.getItem('jwt')
  });
  public url = 'https://localhost:5001/api/Users';
  public isConnected: boolean;
  constructor(protected http: HttpClient) {
    this.isConnected = this.checkJwt();
  }

  public signUp(form: any, typeAppel: string): Observable<any> {
    let $url = 'https://localhost:5001/api/';
    if (typeAppel === 'association' || typeAppel === 'entreprise') {
      $url += 'Associations';
    } else {
      $url = this.url;
    }
    return this.http
        .post<any>(`${$url}/SignUp`, JSON.stringify(form), {headers: this.headers})
        .pipe(
            tap((response: any) => {
                return response;
            }),
        );
  }

  public login(form: any, typeAppel: string): Observable<any> {
    let $url = 'https://localhost:5001/api/';
    if (typeAppel === 'association' || typeAppel === 'entreprise') {
      $url += 'Associations';
    } else {
      $url = this.url;
    }
    console.log('form', JSON.stringify(form));
    return this.http
      .post<any>(`${$url}/Login`, JSON.stringify(form), {headers: this.headers})
      .pipe(
          tap((response: any) => {
              return response;
          }),
      );
  }

  public getUser(id: string): Observable<any> {
    return this.http
      .get<any>(`${this.url}/${id}`)
      .pipe(
          tap((response: any) => {
              return response;
          }),
      );
  }

  public edit(form: any): Observable<any> {
    console.log('form', JSON.stringify(form));
    return this.http
      .put<any>(`${this.url}/EditProfile`, JSON.stringify(form), {headers: this.headers})
      .pipe(
        tap((response: any) => {
            console.log('test');
            return response;
        }),
    );
  }

  public deleteProfile(id: number): Observable<any> {
    return this.http
      .delete<any>(`${this.url}/${id}`)
      .pipe(
        tap((response: any) => {
            return response;
        }),
    );
  }

  public checkJwt(): boolean {
    const $jwt = localStorage.getItem('jwt');
    if ($jwt) {
      return true;
    } else {
      return false;
    }
  }
}