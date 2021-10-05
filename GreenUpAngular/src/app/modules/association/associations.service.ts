import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class AssociationsService {
  public headers = new HttpHeaders({
    'Content-Type': 'application/json'
  });
  constructor(protected http: HttpClient) {} 
  public url = 'https://localhost:5001​/api​/Associations'; 

  public getAllAssociations(): Observable<any[]> {
    return this.http.get<any[]>(this.url);
  }

  public signUp(form: any): Observable<any> {
    console.log('form', JSON.stringify(form));
    return this.http
        .post<any>('https://localhost:5001/api/Associations/SignUp', JSON.stringify(form), {headers: this.headers})
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
      .post<any>('https://localhost:5001/api/Associations/Login', JSON.stringify(form), {headers: this.headers})
      .pipe(
          tap((response: any) => {
              console.log('test');
              return response;
          }),
      );
  }
}