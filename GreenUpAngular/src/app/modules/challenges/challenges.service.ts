import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

const baseUrl = `${environment.apiURL}`;

@Injectable({
  providedIn: 'root',
})
export class ChallengesService {
  public url = `${baseUrl}/api/Challenges`; 
  constructor(protected http: HttpClient) {

  }

  public getAllChallenges(): Observable<any[]> {
    return this.http.get<any[]>(this.url);
  }
}