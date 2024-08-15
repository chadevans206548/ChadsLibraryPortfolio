import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTable, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';
import { BookViewModel } from '../interfaces';
import { CommonModule } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { BooksViewModelDataSource } from './books-datasource';
import { MatIcon } from '@angular/material/icon';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

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
    MatIcon,
    FormsModule,
    ReactiveFormsModule,
    MatSlideToggleModule,
  ],
})
export class FeaturedBooksComponent implements OnInit {
  books: BookViewModel[] = [];
  filteredBooks: BookViewModel[] = [];
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<BookViewModel>;
  dataSource = new BooksViewModelDataSource([]);

  listDisplay = 'flex';
  gridDisplay = 'none';
  paginatorDisplay = 'none';
  searchMode = true;
  searchForm: FormGroup = new FormGroup({
    searchText: new FormControl(''),
    isAvailable: new FormControl(''),
  });

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['title', 'author', 'description', 'rating', 'available', 'action'];

  constructor(private dataService: DataService, private router: Router) {}

  ngOnInit(): void {
    this.dataService.getFeaturedBooks().subscribe((x) => {
      this.dataSource = new BooksViewModelDataSource(x);
      // this.filteredDataSource = new BooksViewModelDataSource(x);
      this.books = x;
      this.filteredBooks = x;

      this.dataSource.sort = this.sort;
      this.dataSource.paginator = this.paginator;
      this.table.dataSource = this.dataSource;
    });
  }

  viewBook(bookId: number) {
    this.router.navigate(['/book', bookId]);
  }

  toggleListView() {
    this.listDisplay = 'flex';
    this.gridDisplay = 'none';
    this.paginatorDisplay = 'none';
  }

  toggleGridView() {
    this.listDisplay = 'none';
    this.gridDisplay = 'table';
    this.paginatorDisplay = 'block';
  }

  cancel() {
    this.searchMode = true;
    var ctrl = this.searchForm?.controls['searchText'];
    ctrl.setValue('');
    this.filter('');
  }

  search() {
    this.searchMode = false;
    var ctrl = this.searchForm?.controls['searchText'];
    this.filter(ctrl.value);
  }

  searchAvailable() {
    this.searchMode = true;
    var ctrl = this.searchForm?.controls['searchText'];
    this.filter(ctrl.value);
  }

  filter(searchText: string) {
    this.filteredBooks = this.books;
    var ctrl = this.searchForm?.controls['isAvailable'];

    if (ctrl.value) {
      this.filteredBooks = this.filteredBooks.filter(
        (x) =>
          (x.title.includes(searchText) || x.author.includes(searchText)) &&
          x.available == true
      );
    } else {
      this.filteredBooks = this.filteredBooks.filter(
        (x) => x.title.includes(searchText) || x.author.includes(searchText)
      );
    }

    if (ctrl.value) {
      this.table.dataSource = this.dataSource.data.filter(
        (x) =>
          (x.title.includes(searchText) || x.author.includes(searchText)) &&
          x.available == true
      );
    } else {
      this.table.dataSource = this.dataSource.data.filter(
        (x) => x.title.includes(searchText) || x.author.includes(searchText)
      );
    }
  }
}
