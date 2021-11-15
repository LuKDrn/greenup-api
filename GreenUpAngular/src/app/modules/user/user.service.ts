import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  public headers = new HttpHeaders({
    'Content-Type': 'application/json'
  });
  public url = 'https://localhost:5001/api/Users';
  constructor(protected http: HttpClient) {}

  public signUp(form: any): Observable<any> {
    console.log('form', JSON.stringify(form));
    return this.http
        .post<any>(`${this.url}/SignUp`, JSON.stringify(form), {headers: this.headers})
        .pipe(
            tap((response: any) => {
                return response;
            }),
        );
  }

  public login(form: any): Observable<any> {
    console.log('form', JSON.stringify(form));
    return this.http
      .post<any>(`${this.url}/Login`, JSON.stringify(form), {headers: this.headers})
      .pipe(
          tap((response: any) => {
              console.log('test');
              return response;
          }),
      );
  }

  public getUser(): Observable<any> {
    return this.http
      .get<any>(`${this.url}/Login`)
      .pipe(
          tap((response: any) => {
              console.log('test');
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
    console.log(id);
    return this.http
      .delete<any>(`${this.url}/${id}`)
      .pipe(
        tap((response: any) => {
            console.log('test');
            return response;
        }),
    );
  }
}