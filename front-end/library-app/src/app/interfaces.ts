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

  export interface AddUserViewModel {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    role: string;
}

export interface RegistrationResponseViewModel {
  isSuccessfulRegistration: boolean;
  errros: string[];
}

export interface AuthenticateUserViewModel {
  email: string;
  password: string;
}

export interface AuthenticationResponseViewModel {
  isAuthSuccessful: boolean;
  errorMessage: string;
  token: string;
}
