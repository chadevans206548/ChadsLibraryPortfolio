using System.Net;
using AutoMapper;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Model.Entities;
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
        try
        {
            var book = this._mapper.Map<Book>(addBookViewModel);
            var entity = await this._libraryContext.AddAsync(book);
            await this._libraryContext.SaveChangesAsync();
            return this._mapper.Map<BookViewModel>(entity.Entity);
        }
        catch (Exception e)
        {

            throw e;
        }
    }

    public async Task<bool> DeleteBook(int bookId)
    {
        if (this._libraryContext.Books.Any(x => x.BookId == bookId))
        {
            var entity = await this._libraryContext.Books.FirstOrDefaultAsync(x => x.BookId == bookId);
            this._libraryContext.Remove(entity);
            await this._libraryContext.SaveChangesAsync();
            return true;
        }
        else
        { return false; }
    }

    public async Task<BookViewModel> EditBook(EditBookViewModel editBookViewModel)
    {
        var entity = await this._libraryContext.Books.FirstOrDefaultAsync(x => x.BookId == editBookViewModel.BookId);
        this._mapper.Map(editBookViewModel, entity);
        this._libraryContext.Update(entity);
        await this._libraryContext.SaveChangesAsync();
        return this._mapper.Map<BookViewModel>(entity);
    }

    public async Task<BookViewModel> GetBook(int bookId)
    {
        var book = await this._libraryContext.Books
            .Include(x => x.InventoryLogs)
            .Include(x => x.Reviews)
            .Where(x => x.BookId == bookId)
            .FirstOrDefaultAsync();
        return this._mapper.Map<BookViewModel>(book);
    }

    public async Task<List<string>> GetAllBookTitles()
    {
        return await this._libraryContext.Books.Select(x => x.Title).ToListAsync();
    }

}
