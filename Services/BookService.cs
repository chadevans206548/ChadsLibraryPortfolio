using AutoMapper;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Model.Entities;
using ChadsLibraryPortfolio.Models;
using ChadsLibraryPortfolio.ViewModels.Books;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using ViewModels.Books.Validators;
using ViewModels.Common;

namespace ChadsLibraryPortfolio.Services;

public class BookService(LibraryContext libraryContext, IMapper mapper) : IBookService
{
    protected readonly LibraryContext _libraryContext = libraryContext;
    protected readonly IMapper _mapper = mapper;

    public async Task<BookViewModel> AddBook(AddBookViewModel addBookViewModel)
    {
        AddBookValidator validator = new AddBookValidator(this);
        ValidationResult result = await validator.ValidateAsync(addBookViewModel);

        if (result.IsValid)
        {
            var book = this._mapper.Map<Book>(addBookViewModel);
            var entity = await this._libraryContext.AddAsync(book);
            await this._libraryContext.SaveChangesAsync();
            return this._mapper.Map<BookViewModel>(entity.Entity);
        }
        else
        {
            return new BookViewModel();
        }
    }

    public async Task<ValidationResultViewModel> ValidateAddBook(AddBookViewModel addBookViewModel)
    {
        AddBookValidator validator = new AddBookValidator(this);
        ValidationResult result = await validator.ValidateAsync(addBookViewModel);
        return this._mapper.Map<ValidationResultViewModel>(result);
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
        EditBookValidator validator = new EditBookValidator(this);
        ValidationResult result = await validator.ValidateAsync(editBookViewModel);

        if (result.IsValid)
        {
            var entity = await this._libraryContext.Books.FirstOrDefaultAsync(x => x.BookId == editBookViewModel.BookId);
            this._mapper.Map(editBookViewModel, entity);
            this._libraryContext.Update(entity);
            await this._libraryContext.SaveChangesAsync();
            return this._mapper.Map<BookViewModel>(entity);
        }
        else
        {
            return new BookViewModel();
        }
    }

    public async Task<ValidationResultViewModel> ValidateEditBook(EditBookViewModel editBookViewModel)
    {
        EditBookValidator validator = new EditBookValidator(this);
        ValidationResult result = await validator.ValidateAsync(editBookViewModel);
        return this._mapper.Map<ValidationResultViewModel>(result);
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

    public async Task<List<BookViewModel>> GetFeaturedBooks()
    {
        var books = await this._libraryContext.Books
            .Include(x => x.InventoryLogs)
            .Include(x => x.Reviews)
            .Where(x => x.Reviews.Sum(x => x.Rating) > 0)
            .ToListAsync();
        return this._mapper.Map<List<BookViewModel>>(books);
    }

    public async Task<List<BookViewModel>> GetAllBooks()
    {
        var books = await this._libraryContext.Books
            .Include(x => x.InventoryLogs)
            .Include(x => x.Reviews)
            .ToListAsync();
        return this._mapper.Map<List<BookViewModel>>(books);
    }

    public async Task<BookViewModel> GetBookByTitle(string title)
    {
        var book = await this._libraryContext.Books
            .Include(x => x.InventoryLogs)
            .Include(x => x.Reviews)
            .Where(x => x.Title.ToUpper() == title.ToUpper())
            .FirstOrDefaultAsync();
        return this._mapper.Map<BookViewModel>(book);
    }

    public async Task<BookViewModel> GetBookByIsbn(string isbn)
    {
        var book = await this._libraryContext.Books
            .Include(x => x.InventoryLogs)
            .Include(x => x.Reviews)
            .Where(x => x.Isbn.ToUpper() == isbn.ToUpper())
            .FirstOrDefaultAsync();
        return this._mapper.Map<BookViewModel>(book);
    }

    public async Task<List<string>> GetAllBookTitles()
    {
        return await this._libraryContext.Books.Select(x => x.Title).ToListAsync();
    }

}
