import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableModule, MatTable } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { BooksViewModelDataSource } from './books-datasource';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';
import { BookViewModel } from '../interfaces';

@Component({
  selector: 'app-featured-books',
  templateUrl: './featured-books.component.html',
  styleUrl: './featured-books.component.scss',
  standalone: true,
  imports: [MatTableModule, MatPaginatorModule, MatSortModule],
})
export class FeaturedBooksComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<BookViewModel>;
  dataSource = new BooksViewModelDataSource([]);

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['title', 'author', 'description', 'rating', 'action'];

  constructor(private dataService: DataService, private router: Router) {}

  ngOnInit(): void {
    this.dataService.getFeaturedBooks().subscribe((x) => {
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
