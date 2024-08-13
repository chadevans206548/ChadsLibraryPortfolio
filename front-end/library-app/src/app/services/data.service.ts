import { HttpClient } from '@angular/common/http';
import { DestroyRef, Inject, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { BookViewModel, ReviewListItem } from '../interfaces';
@Injectable({
  providedIn: 'root',
})
export class DataService {
  private http: HttpClient;
  private baseUrl: string;
  private _destroyRef = inject(DestroyRef);

  constructor(@Inject(HttpClient) http: HttpClient) {
    this.http = http;
    this.baseUrl = 'https://localhost:44316/api';
  }

  getBook(bookId: number): Observable<BookViewModel> {
    let url_ = this.baseUrl + '/Book/GetBook/{bookId}';
    if (bookId === undefined || bookId === null)
      throw new Error("The parameter 'bookId' must be defined.");
    url_ = url_.replace('{bookId}', encodeURIComponent('' + bookId));
    url_ = url_.replace(/[?&]$/, '');

    return this.http.get<BookViewModel>(url_);
  }

  getFeaturedBooks(): Observable<BookViewModel[]> {
    let url_ = this.baseUrl + '/Book/GetFeaturedBooks';

    return this.http.get<BookViewModel[]>(url_);
  }

  getAllBooks(): Observable<BookViewModel[]> {
    let url_ = this.baseUrl + '/Book/GetAllBooks';

    return this.http.get<BookViewModel[]>(url_);
  }

  getReviewsByBook(bookId: number): Observable<ReviewListItem[]> {
    let url_ = this.baseUrl + '/Review/GetReviewsByBook/{bookId}';
    if (bookId === undefined || bookId === null)
      throw new Error("The parameter 'bookId' must be defined.");
    url_ = url_.replace('{bookId}', encodeURIComponent('' + bookId));
    url_ = url_.replace(/[?&]$/, '');

    return this.http.get<ReviewListItem[]>(url_);
  }

  checkout(bookId: number) {
    let url_ = this.baseUrl + '/InventoryLog/Checkout';
    if (bookId === undefined || bookId === null)
      throw new Error("The parameter 'bookId' must be defined.");
    url_ = url_.replace('{bookId}', encodeURIComponent('' + bookId));
    url_ = url_.replace(/[?&]$/, '');

    return this.http.put<number>(url_, bookId);
  }

  checkin(bookId: number) {
    let url_ = this.baseUrl + '/InventoryLog/Checkin';
    if (bookId === undefined || bookId === null)
      throw new Error("The parameter 'bookId' must be defined.");
    url_ = url_.replace('{bookId}', encodeURIComponent('' + bookId));
    url_ = url_.replace(/[?&]$/, '');

    return this.http.put<number>(url_, bookId);
  }
}
