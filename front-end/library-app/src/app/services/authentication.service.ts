import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
  AddUserViewModel,
  AuthenticateUserViewModel,
  AuthenticationResponseViewModel,
  RegistrationResponseViewModel,
} from '../interfaces';
import { Subject } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthenticationService {
  private baseUrl: string;
  private authChangeSub = new Subject<boolean>();
  public authChanged = this.authChangeSub.asObservable();

  constructor(private http: HttpClient, private jwtHelper: JwtHelperService) {
    this.baseUrl = 'https://localhost:44316/api';
  }

  public isUserAuthenticated = (): boolean => {
    const token = localStorage.getItem('token');
    let tokenValid: boolean = false;
    if (token) {
      tokenValid = true;
    }
    return tokenValid && !this.jwtHelper.isTokenExpired(token);
  };

  public isUserLibrarian = (): boolean => {
    const token = localStorage.getItem("token") ?? '';
    const decodedToken = this.jwtHelper.decodeToken(token);

    if(decodedToken) {
      const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']
      return role === 'Librarian';        
    }

    return false;
  }

  public sendAuthStateChangeNotification = (isAuthenticated: boolean) => {
    this.authChangeSub.next(isAuthenticated);
  };

  public registerUser = (body: AddUserViewModel) => {
    let url_ = this.baseUrl + '/accounts/Registration';
    return this.http.post<RegistrationResponseViewModel>(url_, body);
  };

  public loginUser = (body: AuthenticateUserViewModel) => {
    let url_ = this.baseUrl + '/accounts/Login';
    return this.http.post<AuthenticationResponseViewModel>(url_, body);
  };

  public logout = () => {
    localStorage.removeItem('token');
    this.sendAuthStateChangeNotification(false);
  };
}
