import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

const baseUrl = `${environment.apiURL}`;

@Injectable({
  providedIn: 'root',
})
export class ProduitsService {
  public url = `${baseUrl}/api/Produits`; 
  constructor(protected http: HttpClient) {

  }

  public getAllProduct(): Observable<any[]> {
    return this.http.get<any[]>(this.url);
  }
}