import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { AddUserViewModel } from '../interfaces';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router';
import { MatOption } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';

@Component({
  selector: 'app-register-user',
  standalone: true,
  templateUrl: './register-user.component.html',
  styleUrls: [],
  imports: [
    ReactiveFormsModule,
    CommonModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    MatCardModule,
    MatOption,
    MatFormField,
    MatSelectModule,
    FormsModule,
  ],
})
export class RegisterUserComponent {
  options: string[] = ['Customer', 'Librarian'];
  registerForm: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    role: new FormControl('', [Validators.required]),
  });

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {}

  public validateControl = (controlName: string) => {
    var ctrl = this.registerForm?.controls[controlName];
    return ctrl && ctrl.invalid && ctrl.touched;
  };

  public hasError = (controlName: string, errorName: string) => {
    var ctrl = this.registerForm?.controls[controlName];
    return ctrl && ctrl.hasError(errorName);
  };

  public registerUser = (registerFormValue: any) => {
    const formValues = { ...registerFormValue };
    const user: AddUserViewModel = {
      firstName: formValues.firstName,
      lastName: formValues.lastName,
      email: formValues.email,
      password: formValues.password,
      role: formValues.role,
    };

    this.authService.registerUser(user).subscribe({
      next: (_) => this.router.navigate(['/login']),
      error: (err: HttpErrorResponse) => console.log(err.error.errors),
    });
  };
}
