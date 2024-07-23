using ChadsLibraryPortfolio.ViewModels.Books;
using ViewModels.Common;

namespace ChadsLibraryPortfolio.Interfaces;

public interface IBookService
{
    Task<BookViewModel> AddBook(AddBookViewModel addBookViewModel);
    Task<ValidationResultViewModel> ValidateAddBook(AddBookViewModel addBookViewModel);
    Task<BookViewModel> EditBook(EditBookViewModel editBookViewModel);
    Task<ValidationResultViewModel> ValidateEditBook(EditBookViewModel editBookViewModel);
    Task<bool> DeleteBook(int bookId);
    Task<BookViewModel> GetBook(int bookId);
    Task<BookViewModel> GetBookByTitle(string title);
    Task<BookViewModel> GetBookByIsbn(string isbn);
    Task<List<string>> GetAllBookTitles();
    Task<List<BookViewModel>> GetFeaturedBooks();
    Task<List<BookViewModel>> GetAllBooks();
}
