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
  constructor(protected http: HttpClient) {}

  public signUp(form: any): Observable<any> {
    console.log('form', form);
    console.log('form', JSON.stringify(form));
    return this.http
        .post<any>('https://localhost:5001/api/Users/SignUp', form, {headers: this.headers})
        .pipe(
            tap((response: any) => {
                console.log('test');
                return response;
            }),
        );
  }

  public login(form: any): Observable<any> {
    console.log('form', JSON.stringify(form));
    return this.http
      .post<any>('https://localhost:5001/api/Users/Login', JSON.stringify(form), {headers: this.headers})
      .pipe(
          tap((response: any) => {
              console.log('test');
              return response;
          }),
      );
  }

  public getUser(id: string): Observable<any> {
    return this.http
      .get<any>(`https://localhost:5001/api/Users/${id}`)
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
      .put<any>('https://localhost:5001/api/Users/EditProfile', JSON.stringify(form), {headers: this.headers})
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
      .delete<any>(`https://localhost:5001/api/Users/${id}`)
      .pipe(
        tap((response: any) => {
            console.log('test');
            return response;
        }),
    );
  }
}