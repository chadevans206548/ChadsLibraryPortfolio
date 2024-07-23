import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableModule, MatTable } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import {
  BooksViewModelDataSource,
  BookViewModel,
} from '../featured-books/books-datasource';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-search-books',
  templateUrl: './search-books.component.html',
  styleUrl: './search-books.component.scss',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatSortModule],
})
export class SearchBooksComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<BookViewModel>;
  dataSource = new BooksViewModelDataSource([]);

  constructor(private dataService: DataService, private router: Router) {}

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['title', 'author', 'description', 'rating', 'action'];

  ngOnInit(): void {
    this.dataService.getAllBooks().subscribe((x) => {
      this.dataSource = new BooksViewModelDataSource(x);

      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      this.table.dataSource = this.dataSource;
    });
  }

  viewBook(bookId: number) {
    this.router.navigate(['/book', bookId]);
  }
}
