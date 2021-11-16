import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class MissionsService {
  public headers = new HttpHeaders({
    'Content-Type': 'application/json'
  });
  public urlMissions = 'https://localhost:5001/api/Missions'; 
  constructor(protected http: HttpClient) {}

  public getAllMissions(): Observable<any[]> {
    return this.http.get<any[]>(this.urlMissions);
  }

  //Voir les params 
  public getOneMissions(id: string): Observable<any> {
    return this.http.get<any[]>(`${this.urlMissions}/${id}`);
  }

  public addMission(form: any): Observable<any> {
    console.log('form', JSON.stringify(form));
    return this.http
      .post<any>(`${this.urlMissions}/Add`, JSON.stringify(form), {headers: this.headers})
      .pipe(
        tap((response: any) => {
          console.log('ajout réussi');
          return response;
        })
      );
  }

  public updateMission(form: any): Observable<any> {
    console.log('form', JSON.stringify(form));
    return this.http
      .post<any>(`${this.urlMissions}/Update`, JSON.stringify(form), {headers: this.headers})
      .pipe(
        tap((response: any) => {
          console.log('modification réussi');
          return response;
        })
      );
  }
}