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
  public missionApi = 'https://localhost:5001/api/Missions'; 
  constructor(protected http: HttpClient) {

  }

  public getAllMissions(): Observable<any[]> {
    return this.http.get<any[]>(this.missionApi);
  }

  //Voir les params 
  public getOneMission(id: number): Observable<any> {
    return this.http.get<any[]>(`${this.missionApi}/${id}`);
  }

  public addMission(form: any): Observable<any> {
    console.log('form', JSON.stringify(form));
    return this.http
      .post<any>(`${this.missionApi}/Add`, JSON.stringify(form), {headers: this.headers})
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
      .post<any>(`${this.missionApi}/Update`, JSON.stringify(form), {headers: this.headers})
      .pipe(
        tap((response: any) => {
          console.log('modification réussi');
          return response;
        })
      );
  }
}