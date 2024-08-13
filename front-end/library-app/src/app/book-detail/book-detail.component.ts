import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from '../services/data.service';
import { BookViewModel } from '../featured-books/books-datasource';
import { DatePipe } from '@angular/common';
import { ReviewListComponent } from '../review-list/review-list.component';

@Component({
  selector: 'app-book-detail',
  standalone: true,
  imports: [ReviewListComponent],
  templateUrl: './book-detail.component.html',
  styleUrl: './book-detail.component.scss',
})
export class BookDetailComponent implements OnInit, AfterViewInit {
  bookId = 0;
  book: BookViewModel | undefined;

  constructor(
    private route: ActivatedRoute,
    private dataService: DataService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.bookId = params['id'];
    });
  }

  ngAfterViewInit(): void {
    this.dataService.getBook(this.bookId).subscribe((x) => {
      this.book = x;
    });
  }

  checkout() {
    if (this.book.available) {
      this.dataService.checkout(this.bookId);
      this.book.available = false;
    }
  }

  checkin() {
    if (!this.book.available) {
      this.dataService.checkin(this.bookId);
      this.book.available = true;
    }
  }
}
