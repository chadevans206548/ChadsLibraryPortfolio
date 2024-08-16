import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableModule, MatTable } from '@angular/material/table';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { BooksViewModelDataSource } from '../featured-books/books-datasource';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';
import { BookViewModel } from '../interfaces';
import { CommonModule } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import { AuthenticationService } from '../services/authentication.service';

@Component({
  selector: 'app-search-books',
  templateUrl: './search-books.component.html',
  styleUrl: './search-books.component.scss',
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
  ],
})
export class SearchBooksComponent implements OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<BookViewModel>;
  dataSource = new BooksViewModelDataSource([]);
  searchMode = true;
  searchForm: FormGroup = new FormGroup({
    searchText: new FormControl(''),
  });
  isLibrarian = false;

  constructor(
    private dataService: DataService,
    private router: Router,
    private authService: AuthenticationService
  ) {}

  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['title', 'author', 'description', 'rating', 'action'];

  ngOnInit(): void {
    if (this.authService.isUserLibrarian()) {
      this.isLibrarian = true;
    }
    this.getBookData();
  }

  getBookData() {
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

  editBook(bookId: number) {
    this.router.navigate(['/edit-book', bookId]);
  }  

  deleteBook(bookId: number) {
    this.dataService.deleteBook(bookId).subscribe((x) => {
      this.getBookData();
    });
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

  filter(searchText: string) {
    this.table.dataSource = this.dataSource.data.filter((x) =>
      x.title.includes(searchText)
    );
  }
}
