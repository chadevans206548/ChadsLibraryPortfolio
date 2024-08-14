import { Routes, withComponentInputBinding } from '@angular/router';
import { FeaturedBooksComponent } from './featured-books/featured-books.component';
import { SearchBooksComponent } from './search-books/search-books.component';
import { ManageBooksComponent } from './manage-books/manage-books.component';
import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { BookDetailComponent } from './book-detail/book-detail.component';
import { RegisterUserComponent } from './register-user/register-user.component';

export const routes: Routes = [
  {
    path: 'register',
    component: RegisterUserComponent,
    data: {},
  },
  {
    path: 'featured',
    component: FeaturedBooksComponent,
    data: {},
  },
  {
    path: 'search',
    component: SearchBooksComponent,
    data: {},
  },
  {
    path: 'manage',
    component: ManageBooksComponent,
    data: {},
  },
  {
    path: 'book/:id',
    component: BookDetailComponent,
    data: {},
  },
  { path: '**', component: FeaturedBooksComponent },
];

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes, withComponentInputBinding())],
};
