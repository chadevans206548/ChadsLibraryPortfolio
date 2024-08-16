import { Routes, withComponentInputBinding } from '@angular/router';
import { FeaturedBooksComponent } from './featured-books/featured-books.component';
import { SearchBooksComponent } from './search-books/search-books.component';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { RegisterUserComponent } from './register-user/register-user.component';
import { LoginComponent } from './login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './_guards/auth-guard.guard';
import { ForbiddenComponent } from './forbidden/forbidden.component';
import { LibrarianGuard } from './_guards/librarian-guard.guard';
import { LeaveReviewComponent } from './leave-review/leave-review.component';
import { AddBookComponent } from './add-book/add-book.component';
import { EditBookComponent } from './edit-book/edit-book.component';

export const routes: Routes = [
  {
    path: 'register',
    component: RegisterUserComponent,
    data: {},
  },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent },
  { path: 'forbidden', component: ForbiddenComponent },
  {
    path: 'featured',
    component: FeaturedBooksComponent,
    canActivate: [AuthGuard],
    data: {},
  },
  {
    path: 'search',
    component: SearchBooksComponent,
    canActivate: [AuthGuard],
    data: {},
  },
  {
    path: 'add-book',
    component: AddBookComponent,
    canActivate: [AuthGuard, LibrarianGuard],
    data: {},
  },
  {
    path: 'edit-book/:id',
    component: EditBookComponent,
    canActivate: [AuthGuard, LibrarianGuard],
    data: {},
  },
  {
    path: 'book/:id',
    component: BookDetailComponent,
    canActivate: [AuthGuard],
    data: {},
  },
  {
    path: 'leave-review/:id',
    component: LeaveReviewComponent,
    canActivate: [AuthGuard],
    data: {},
  },  
  { path: '**', component: HomeComponent },
];

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes, withComponentInputBinding())],
};
