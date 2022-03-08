import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { ForgotIdentifiantComponent } from 'src/app/component/dialog/forgot-identifiant/forgot-identifiant.component';
import { SharedService } from '../../../shared.service';
import { UserService } from '../user.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.scss']
})
export class AuthComponent implements OnInit {
  public invalidLogin!: boolean;
  public form!: FormGroup;
  public typeLogin: string;
  public hide = true;

  constructor(
    private router: Router, 
    private aRoute: ActivatedRoute,
    private http: HttpClient,
    private service: SharedService,
    private fb: FormBuilder,
    private userService: UserService,
    private dialog: MatDialog,
    private snackBar: MatSnackBar,
    private sharedService: SharedService
  ) {

    this.typeLogin = this.aRoute.snapshot.params['id'];
    this.initForm();
  }

  ngOnInit(): void { }

  public getErrorMessage() {
    if (this.form.hasError('required')) {
      return 'Vous devez saisir quelque chose';
    }
    return this.form.hasError('email') ? 'Veuillez saisir une adresse mail valide' : '';
  }

  public onSubmit(): void { }

  public login(): void {
    this.sharedService.isLoading = true;
    this.userService.login(this.form.value, this.typeLogin).subscribe(
    response => {
      console.log(response);
      const token = (<any>response).token;
      localStorage.setItem("jwt", token);
      this.invalidLogin = false;
      this.sharedService.isLoading = false;
      this.router.navigate(["/"]);
      this.snackBar.open('Vous êtes connecté', 'OK', {
        duration: 3000
      });
    }, err => {
      this.invalidLogin = true;
      this.snackBar.open(err.error, 'OK', {
        duration: 3000
      });
    });
  }

  public signUp(): void {
    this.router.navigate(['/signUp', `${this.typeLogin}`]); 
  }

  public forgotIdent(): void {
    const dialogRef = this.dialog.open(ForgotIdentifiantComponent, {
      hasBackdrop: true,
      disableClose: true,
      position: { top: '80px' },
      width: '500px',
      panelClass: 'app-dialog',
      data: {}
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }

  private initForm(): void {
    this.form = this.fb.group({
      mail: new FormControl(''),
      rnaNumber: new FormControl(''),
      password: new FormControl('', [Validators.required])
    });
  }
}