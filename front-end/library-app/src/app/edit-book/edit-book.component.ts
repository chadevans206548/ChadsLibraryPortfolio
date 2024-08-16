import { CommonModule } from '@angular/common';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import {
  EditBookViewModel,
  BookViewModel,
  ValidationResultViewModel,
} from '../interfaces';
import { DataService } from '../services/data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { catchError, concatMap, map, Observable, retry } from 'rxjs';
import { title } from 'process';

@Component({
  selector: 'app-edit-book',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    CommonModule,
    MatInputModule,
    MatFormFieldModule,
    MatFormField,
    FormsModule,
    MatCardModule,
    MatSelectModule,
    MatButtonModule,
  ],
  templateUrl: './edit-book.component.html',
  styleUrl: './edit-book.component.scss',
})
export class EditBookComponent implements OnInit, AfterViewInit {
  bookId = 0;
  book: BookViewModel | undefined;

  editBookForm: FormGroup = new FormGroup({
    bookId: new FormControl(''),
    title: new FormControl('', [Validators.required]),
    author: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required]),
    coverImage: new FormControl('', [Validators.required]),
    publisher: new FormControl('', [Validators.required]),
    publicationDate: new FormControl('', [Validators.required]),
    category: new FormControl('', [Validators.required]),
    isbn: new FormControl('', [Validators.required]),
    pageCount: new FormControl('', [Validators.required]),
  });
  validationResults: ValidationResultViewModel = {
    errorMessages: [],
    isValid: true,
  };

  constructor(
    private dataService: DataService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.bookId = params['id'];
    });
  }

  ngAfterViewInit(): void {
    this.dataService.getBook(this.bookId).subscribe((x) => {
      this.book = x;

      this.editBookForm.setValue({
        bookId: this.book.bookId,
        title: this.book.title,
        author: this.book.author,
        description: this.book.description,
        coverImage: this.book.coverImage,
        publisher: this.book.publisher,
        publicationDate: this.book.publicationDate,
        isbn: this.book.isbn,
        category: this.book.category,
        pageCount: this.book.pageCount,
      });
    });
  }

  setValue(controlName: string, value: any) {
    var ctrl = this.editBookForm.controls[controlName];
    ctrl.setValue(value);
  }

  public editBook = (editBookFormValues: any) => {
    const formValues = { ...editBookFormValues };
    const book: EditBookViewModel = {
      bookId: this.bookId,
      title: formValues.title,
      author: formValues.author,
      description: formValues.description,
      coverImage: formValues.coverImage,
      publisher: formValues.publisher,
      publicationDate: formValues.publicationDate,
      category: formValues.category,
      isbn: formValues.isbn,
      pageCount: formValues.pageCount,
    };

    this.validateAndSave(book).subscribe((x) => {
      if (x.response1.isValid) {
        this.router.navigate(['/book', x.response2.bookId]);
      } else {
        this.validationResults = x.response1;
      }
    });
  };

  validateAndSave(book: EditBookViewModel): Observable<any> {
    return this.dataService
      .validateEditBook(book)
      .pipe(
        concatMap((evt) =>
          this.dataService
            .editBook(book)
            .pipe(map((res) => ({ response1: evt, response2: res })))
        )
      );
  }
}
