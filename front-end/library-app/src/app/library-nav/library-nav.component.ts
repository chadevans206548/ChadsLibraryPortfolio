import { Component, inject } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { AsyncPipe, CommonModule } from '@angular/common';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import {
  Router,
  RouterLink,
  RouterLinkActive,
  RouterOutlet,
} from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-library-nav',
  templateUrl: './library-nav.component.html',
  styleUrl: './library-nav.component.scss',
  standalone: true,
  imports: [
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    AsyncPipe,
    RouterOutlet,
    RouterLink,
    RouterLinkActive,
    MatButtonModule,
    MatMenuModule,
    CommonModule,
  ],
})
export class LibraryNavComponent {
  public isUserAuthenticated: boolean = false;
  public isUserLibrarian: boolean = false;

  constructor(
    private authService: AuthenticationService,
    private router: Router
  ) {
    this.authService.authChanged.subscribe((res) => {
      this.isUserAuthenticated = res;
    });
    this.isUserLibrarian = this.authService.isUserLibrarian();
  }

  ngOnInit(): void {
    this.authService.authChanged.subscribe((res) => {
      this.isUserAuthenticated = res;
      this.isUserLibrarian = this.authService.isUserLibrarian();
    });
  }

  public logout = () => {
    this.authService.logout();
    this.router.navigate(['/']);
  };
}
