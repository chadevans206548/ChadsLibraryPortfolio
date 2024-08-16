import { HttpClient, HttpHeaders } from '@angular/common/http';
import { DestroyRef, Inject, Injectable, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { AddBookViewModel, AddReviewViewModel, BookViewModel, EditBookViewModel, ReviewListItem, ValidationResultViewModel } from '../interfaces';
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

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.put<number>(url_, bookId, {headers});
  }

  checkin(bookId: number) {
    let url_ = this.baseUrl + '/InventoryLog/Checkin';
    if (bookId === undefined || bookId === null)
      throw new Error("The parameter 'bookId' must be defined.");

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.put<number>(url_, bookId, {headers});
  }

  addReview(review: AddReviewViewModel) {
    let url_ = this.baseUrl + '/Review/AddReview';
    if (review === undefined || review === null)
      throw new Error("The parameter 'review' must be defined.");

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post<number>(url_, review, {headers});
  }

  deleteBook(bookId: number) {
    let url_ = this.baseUrl + '/Book/DeleteBook/{bookId}';
    if (bookId === undefined || bookId === null)
      throw new Error("The parameter 'bookId' must be defined.");
    url_ = url_.replace('{bookId}', encodeURIComponent('' + bookId));
    url_ = url_.replace(/[?&]$/, '');

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.delete(url_, {headers});
  }

  addBook(book: AddBookViewModel) {
    let url_ = this.baseUrl + '/Book/AddBook';
    if (book === undefined || book === null)
      throw new Error("The parameter 'book' must be defined.");

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post<BookViewModel>(url_, book, {headers});
  }  

  validateAddBook(book: AddBookViewModel) {
    let url_ = this.baseUrl + '/Book/ValidateAddBook';
    if (book === undefined || book === null)
      throw new Error("The parameter 'book' must be defined.");

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post<ValidationResultViewModel>(url_, book, {headers});
  }  

  editBook(book: EditBookViewModel) {
    let url_ = this.baseUrl + '/Book/EditBook';
    if (book === undefined || book === null)
      throw new Error("The parameter 'book' must be defined.");

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.put<BookViewModel>(url_, book, {headers});
  }  

  validateEditBook(book: EditBookViewModel) {
    let url_ = this.baseUrl + '/Book/ValidateEditBook';
    if (book === undefined || book === null)
      throw new Error("The parameter 'book' must be defined.");

    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.put<ValidationResultViewModel>(url_, book, {headers});
  }  

}
