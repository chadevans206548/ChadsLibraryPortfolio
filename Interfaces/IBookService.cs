using ChadsLibraryPortfolio.ViewModels.Books;

namespace ChadsLibraryPortfolio.Interfaces;

public interface IBookService
{
    Task<BookViewModel> AddBook(AddBookViewModel addBookViewModel);
    Task<BookViewModel> EditBook(EditBookViewModel editBookViewModel);
    Task<bool> DeleteBook(int bookId);
    Task<BookViewModel> GetBook(int bookId);
    Task<List<BookViewModel>> SearchBooks(string titleSearch);
}
