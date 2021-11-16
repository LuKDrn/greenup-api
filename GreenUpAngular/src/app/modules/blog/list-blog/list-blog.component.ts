import { Component, OnInit } from '@angular/core';
import { Blog } from 'src/app/model/blog.model';

@Component({
  selector: 'app-list-blog',
  templateUrl: './list-blog.component.html',
  styleUrls: ['./list-blog.component.scss']
})
export class ListBlogComponent implements OnInit {

  public displayAllList: boolean;
  public article: Blog;

  constructor() { 
    this.displayAllList = false;
    this.article = new Blog();
    this.article.uid = 1;
    this.article.categorie = 'environnement';
    this.article.titre = 'Le coût écologique d’un like';
    this.article.date = '26 Avril 2022';
    this.article.resume = 'Quel est le coût écologique de notre activité numérique ? Telle est la principale question à laquelle répond Guillaume Pitron...Quel est le coût écologique de notre activité numérique ? Telle est la principale question à laquelle répond Guillaume Pitron...';
  }

  ngOnInit(): void {
  }

  public displayList(): void {
    this.displayAllList = !this.displayAllList;
  }
}
