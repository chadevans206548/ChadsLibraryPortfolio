import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddUserViewModel, RegistrationResponseViewModel } from '../interfaces';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private baseUrl: string;

  constructor(private http: HttpClient) { 
    this.baseUrl = 'https://localhost:44316/api';
  }

  public registerUser = (body: AddUserViewModel) => {
    let url_ = this.baseUrl + '/accounts/Registration';

    return this.http.post<RegistrationResponseViewModel> (url_, body);
  }
}