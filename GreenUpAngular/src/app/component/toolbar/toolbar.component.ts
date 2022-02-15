import { ChangeDetectorRef, Component, Input, OnChanges, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from 'src/app/modules/user/user.service';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss'],
})
export class ToolbarComponent implements OnInit, OnChanges {
  @Input() isConnected: boolean = false;
  public title: string = 'GreenUp';
  public indexIsActive: number;

  constructor(
    private router: Router,
    private us: UserService,
    private cdr: ChangeDetectorRef
  ) { 
    this.indexIsActive = 0;
  }

  ngOnInit() {
    const $url = this.router.url.toString();
    const $tabUrl = $url.split('/');
    this.cdr.detectChanges();
  }

  ngOnChanges() {
    this.isConnected = this.us.checkJwt();
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

  public aPropos(): void {
    this.router.navigate(['/a-propos']);
  }

  public goToProfile(): void {
    this.router.navigate(['/edit-profile']);
  }

  public isActive(index: number) {
    this.indexIsActive = index; 
  }

  public logout(): void {
    localStorage.removeItem('jwt');
    this.router.navigate(['/home']);
  }
}
