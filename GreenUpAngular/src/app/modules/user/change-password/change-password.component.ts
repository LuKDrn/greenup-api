import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { UserService } from '../user.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./../auth/auth.component.scss']
})
export class ChangePasswordComponent implements OnInit {
  public form!: FormGroup;
  public loading: boolean;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private us: UserService
  ) { 
    this.loading = false;
    const id = this.route.snapshot.params.id;
    this.initForm();
  }

  ngOnInit(): void {
  }

  public save(): void {
    // this.onSubmit();
    console.log('on est la ', this.form);
  }

  public onSubmit(): void {
    this.loading = true;
    this.us.edit(this.form).subscribe(
      (res) => {
        console.log('res', res);
        // this.snackbarOpen(res);
      },
      (error: any) => {
        console.log('error', error);
      }
    );
  }

  private initForm(): void {
    this.form = this.fb.group({
      newPassword: new FormControl('', [Validators.required]),
      confirmPassword: new FormControl('', [Validators.required])
    });
  }

  private snackbarOpen(message: string): void {
    this.snackBar.open(message, 'OK', {
      duration: 3000
    });
  }

}
