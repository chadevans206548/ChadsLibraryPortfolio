import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';
import { BookViewModel } from '../interfaces';
import { CommonModule } from '@angular/common';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-featured-books',
  templateUrl: './featured-books.component.html',
  styleUrl: './featured-books.component.scss',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    CommonModule,
    MatButton,
  ],
})
export class FeaturedBooksComponent implements OnInit {
  books: BookViewModel[] = [];

  constructor(private dataService: DataService, private router: Router) {}

  ngOnInit(): void {
    this.dataService.getFeaturedBooks().subscribe((x) => {
      this.books = x;
    });
  }

  viewBook(bookId: number) {
    this.router.navigate(['/book', bookId]);
  }
}
