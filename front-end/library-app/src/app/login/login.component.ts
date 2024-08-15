import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { ActivatedRoute, Router } from '@angular/router';
import {
  AuthenticateUserViewModel,
  AuthenticationResponseViewModel,
} from '../interfaces';
import { HttpErrorResponse } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatCardModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  private returnUrl = '';

  loginForm: FormGroup = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
  });
  errorMessage: string = '';
  showError = false;

  constructor(
    private authService: AuthenticationService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loginForm;
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  validateControl = (controlName: string) => {
    var ctrl = this.loginForm?.controls[controlName];
    return ctrl && ctrl.invalid && ctrl.touched;
  };

  hasError = (controlName: string, errorName: string) => {
    var ctrl = this.loginForm?.controls[controlName];
    return ctrl && ctrl.hasError(errorName);
  };

  loginUser = (loginFormValue: any) => {
    this.showError = false;
    const login = { ...loginFormValue };

    const userForAuth: AuthenticateUserViewModel = {
      email: login.username,
      password: login.password,
    };

    this.authService.loginUser(userForAuth).subscribe({
      next: (res: AuthenticationResponseViewModel) => {
        localStorage.setItem('token', res.token);
        this.authService.sendAuthStateChangeNotification(res.isAuthSuccessful);
        this.router.navigate([this.returnUrl]);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage = err.message;
        this.showError = true;
      },
    });
  };
}
