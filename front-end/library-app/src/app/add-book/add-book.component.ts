import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
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
  AddBookViewModel,
  BookViewModel,
  ValidationResultViewModel,
} from '../interfaces';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';
import { catchError, concatMap, map, Observable, retry } from 'rxjs';

@Component({
  selector: 'app-add-book',
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
  templateUrl: './add-book.component.html',
  styleUrl: './add-book.component.scss',
})
export class AddBookComponent {
  addBookForm: FormGroup = new FormGroup({
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
    isValid: true
  };

  constructor(private dataService: DataService, private router: Router) {}

  public addBook = (addBookFormValues: any) => {
    const formValues = { ...addBookFormValues };
    const book: AddBookViewModel = {
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
        this.router.navigate(['/book', x.response2.bookId])
      } else {
        this.validationResults = x.response1;
      }
    })
  };

  validateAndSave(book: AddBookViewModel): Observable<any> {
    return this.dataService.validateAddBook(book).pipe(
      concatMap((evt) =>
        this.dataService.addBook(book).pipe(
          map((res) => ({ response1: evt, response2: res }))
        )
      )
    );
  }
  
  
}
