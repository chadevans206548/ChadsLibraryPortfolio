import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { DataService } from '../services/data.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCardModule } from '@angular/material/card';
import { AddReviewViewModel } from '../interfaces';
import { MatSelectModule } from '@angular/material/select';
import { HttpErrorResponse } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';

export interface RatingOptions {
  rating: number,
  description: string
}

@Component({
  selector: 'app-leave-review',
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
    MatButtonModule
  ],
  templateUrl: './leave-review.component.html',
  styleUrl: './leave-review.component.scss',
})
export class LeaveReviewComponent implements OnInit {
  bookId = 0;
  options: RatingOptions[] = [];

  addReviewForm: FormGroup = new FormGroup({
    bookId: new FormControl(''),
    description: new FormControl('', [Validators.required]),
    rating: new FormControl('', [Validators.required]),
  });

  constructor(
    private dataService: DataService,
    private route: ActivatedRoute,
    private router: Router
  ) {

    this.options.push({rating: 1, description: '1 star'});
    this.options.push({rating: 2, description: '2 stars'});
    this.options.push({rating: 3, description: '3 stars'});
    this.options.push({rating: 4, description: '4 stars'});
    this.options.push({rating: 5, description: '5 stars'});

  }

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.bookId = params['id'];
    });    
  }

  public addReview = (addReviewFormValues: any) => {
    const formValues = { ...addReviewFormValues };
    const review: AddReviewViewModel = {
      bookId: this.bookId,
      description: formValues.description,
      rating: formValues.rating.rating,
    };

    this.dataService.addReview(review).subscribe({
      next: (_) => this.router.navigate(['/book', this.bookId]),
      error: (err: HttpErrorResponse) => console.log(err.error.errors),
    });
  };
}
