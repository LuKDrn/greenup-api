import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root',
})
export class ProduitsService {
    public url = 'https://greenup-api.herokuapp.com​/api​/Produits'; 
  constructor(protected http: HttpClient) {

  }

  public getAllProduct(): Observable<any[]> {
    return this.http.get<any[]>(this.url);
  }
}