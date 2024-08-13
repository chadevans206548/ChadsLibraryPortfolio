export interface ReviewListItem {
    reviewId: number;
    bookId: number;
    rating: number;
    description: string;
  }

  export interface BookViewModel {
    bookId: number;
    title: string;
    author: string;
    description: string;
    coverImage: string;
    publisher: string;
    publicationDate: Date | null;
    category: string;
    isbn: string;
    pageCount: number;
    averageUserRating: number;
    available: boolean;
  }

  