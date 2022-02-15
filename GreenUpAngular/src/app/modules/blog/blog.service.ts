import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root',
})
export class BlogService {
  public headers = new HttpHeaders({
      // 'Content-Type': 'application/json',
      'Access-Control_Allow_Origin': 'https://www.actu-environnement.com/',
      'Access-Control-Allow-Headers' : 'Origin, X-Requested-With, Content-Type',
      'Access-Control-Allow-Methods' : 'POST, GET, OPTION',
      'Content-Type':  'application/rss+xml'
    });
    public url = 'https://localhost:5001/api/blog';
    public urlRSS = 'https://www.actu-environnement.com/flux/rss/environnement/';
    constructor(protected http: HttpClient) {}

    getRSSData() {
      this.http
      .get<any>(this.urlRSS, {headers: this.headers})
      .subscribe(data => {
        console.log(data);
        // let parseString = xml2js.parseString;
        // parseString(data, (err, result: NewsRss) => {
        //   this.RssData = result;
        });
      // });
      // , { observe: 'body', headers : this.headers }
      // this.http.get<any>(this.urlRSS).subscribe(
      //   (data) => {
      //   console.log('data', data);
      // },
      // (error) => {
      //   console.log(error);
      // });
    }
}

