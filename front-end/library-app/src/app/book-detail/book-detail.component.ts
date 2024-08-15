import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../services/data.service';
import { ReviewListComponent } from '../review-list/review-list.component';
import { BookViewModel } from '../interfaces';
import { MatButton } from '@angular/material/button';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-book-detail',
  standalone: true,
  imports: [ReviewListComponent, MatButton],
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.scss',
})
export class BookDetailComponent implements OnInit, AfterViewInit {
  bookId = 0;
  book: BookViewModel | undefined;
  isLibrarian = false;

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService,
    private authService: AuthenticationService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.bookId = params['id'];
    });

    if (this.authService.isUserLibrarian()) {
      this.isLibrarian = true;
    }
  }

  ngAfterViewInit(): void {
    this.dataService.getBook(this.bookId).subscribe((x) => {
      this.book = x;
    });
  }

  checkout() {
    if (this.book && this.book.available) {
      this.dataService.checkout(this.bookId);
      this.book.available = false;
    }
  }

  checkin() {
    if (this.book && !this.book.available) {
      this.dataService.checkin(this.bookId);
      this.book.available = true;
    }
  }
}
