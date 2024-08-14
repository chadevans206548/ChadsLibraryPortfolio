import { HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';
import { AddUserViewModel } from '../interfaces';
import { CommonModule } from '@angular/common';
import { MatButton, MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';

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
  ],
})
export class RegisterUserComponent {
  registerForm: FormGroup = new FormGroup({
    firstName: new FormControl(''),
    lastName: new FormControl(''),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    confirm: new FormControl(''),
  });

  constructor(private authService: AuthenticationService) {}

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
      confirmPassword: formValues.confirm,
    };

    this.authService.registerUser(user).subscribe({
      next: (_) => console.log('Successful registration'),
      error: (err: HttpErrorResponse) => console.log(err.error.errors),
    });
  };
}
