import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Blog } from 'src/app/model/blog.model';


@Component({
  selector: 'app-blog',
  templateUrl: './blog.component.html',
  styleUrls: ['./blog.component.scss']
})
export class BlogComponent implements OnInit {

  public article: Blog;
  constructor(
    private router: Router
  ) {
    this.article = new Blog();

    this.article.uid = 1;
    this.article.categorie = 'environnement';
    this.article.titre = 'Le coût écologique d’un like';
    this.article.resume = 'Quel est le coût écologique de notre activité numérique ? Telle est la principale question à laquelle répond Guillaume Pitron...Quel est le coût écologique de notre activité numérique ? Telle est la principale question à laquelle répond Guillaume Pitron...'
    this.article.date = '26 Avril 2022';
    this.article.img = '../../../assets/IllustrationTest.png';
    this.article.corps = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi non massa ut tortor elementum lacinia. Proin a felis ornare, posuere purus a, rhoncus tortor. Nam dui nisl, finibus ut lectus non, accumsan egestas leo. Suspendisse maximus nunc ut porta tempus. Nulla iaculis nulla ut malesuada ornare. Curabitur in hendrerit orci. Nunc gravida a sem vitae scelerisque. Etiam ultricies enim eu placerat porttitor. Morbi mattis sed odio in porttitor. Nunc pellentesque rutrum risus eu rutrum. Donec lobortis cursus erat, et vulputate nisl gravida gravida. Etiam sapien risus, viverra at velit at, consequat porta felis. Sed vehicula tristique mi sed gravida. Nulla convallis pretium mi, eget porttitor ex pulvinar vitae.';
  }

  ngOnInit(): void {
  }

  public goToListBlog(): void {
    this.router.navigate(['/blog']);
  }

}
