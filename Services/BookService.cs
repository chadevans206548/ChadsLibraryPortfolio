using AutoMapper;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Models;
using ChadsLibraryPortfolio.ViewModels.Books;
using Microsoft.EntityFrameworkCore;

namespace ChadsLibraryPortfolio.Services;

public class BookService(LibraryContext libraryContext, IMapper mapper) : IBookService
{
    protected readonly LibraryContext _libraryContext = libraryContext;
    protected readonly IMapper _mapper = mapper;

    public async Task<BookViewModel> AddBook(AddBookViewModel addBookViewModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteBook(int bookId)
    {
        throw new NotImplementedException();
    }

    public Task<BookViewModel> EditBook(EditBookViewModel editBookViewModel)
    {
        throw new NotImplementedException();
    }

    public async Task<BookViewModel> GetBook(int bookId)
    {
        var book = await this._libraryContext.Books.Where(x => x.BookId == bookId).FirstOrDefaultAsync();
        return this._mapper.Map<BookViewModel>(book);
    }

    public Task<List<BookViewModel>> SearchBooks(string titleSearch)
    {
        throw new NotImplementedException();
    }
}
