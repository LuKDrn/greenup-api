import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root',
  })
  export class BlogService {
    public headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': "Bearer "+ localStorage.getItem('jwt')
      });
      public url = 'https://localhost:5001/api/blog';
      constructor(protected http: HttpClient) {}
  }