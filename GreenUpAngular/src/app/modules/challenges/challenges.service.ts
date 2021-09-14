import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ChallengesService {
    public url = 'https://greenup-api.herokuapp.com​/api​/Challenges'; 
  constructor(protected http: HttpClient) {

  }

  public getAllChallenges(): Observable<any[]> {
    return this.http.get<any[]>(this.url);
  }
}