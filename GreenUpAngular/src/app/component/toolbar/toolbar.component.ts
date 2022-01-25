import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
})
export class ToolbarComponent implements OnInit {
  @Input() isConnected: boolean = false;
  public title: string = 'GreenUp';
  public indexIsActive: number;

  constructor(
    private router: Router
  ) { 
    this.indexIsActive = 0;
  }

  ngOnInit() {
    const $url = this.router.url.toString();
    const $tabUrl = $url.split('/');
  }

  public home(): void {
    this.router.navigate(['/']);
  }

  public missions(): void {
    this.router.navigate(['/missions']);
  }

  public produits(): void {
    this.router.navigate(['/produits']);
  }

  public challenges(): void {
    this.router.navigate(['/challenges']);
  }

  public login(typeLogin: string): void {
    this.router.navigate(['/auth', `${typeLogin}`]);
  }

  public associations(): void {
    this.router.navigate(['/associations']);
  }

  public entreprises(): void {
    this.router.navigate(['/auth']);
  }

  public blog(): void {
    this.router.navigate(['/blog']);
  }

  public goToProfile(): void {
    this.router.navigate(['/edit-profile']);
  }

  public isActive(index: number) {
    this.indexIsActive = index; 
  }
}
