import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class AssociationsService {
    public url = 'https://greenup-api.herokuapp.com​/api​/Associations'; 
  constructor(protected http: HttpClient) {

  }

  public getAllAssociations(): Observable<any[]> {
    return this.http.get<any[]>(this.url);
  }
}