import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Blog } from 'src/app/model/blog.model';

@Component({
  selector: 'app-card-blog',
  templateUrl: './card-blog.component.html',
  styleUrls: ['./card-blog.component.scss']
})
export class CardBlogComponent implements OnInit {
  @Input() article?: Blog;
  @Input() format?: string;
  
  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {

  }

  public readMore(): void {
    this.router.navigate(['/blog', this.article?.uid]);
  }
}
