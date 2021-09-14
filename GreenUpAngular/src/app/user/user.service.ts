import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})
export class UserService {

  constructor(protected http: HttpClient) {

  }

  public signUp(form: any): void { //Observable<any> {
    console.log('form', JSON.stringify(form));
    // return this.http
    //     .post<any>('https://greenup-api.herokuapp.com/SignUp', JSON.stringify(form))
    //     .pipe(
    //         tap((response: any) => {
    //             return response;
    //         }),
    //         // catchError(any)
    //     );
  }
}