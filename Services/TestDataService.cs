using Bogus;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Model.Entities;
using ChadsLibraryPortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace ChadsLibraryPortfolio.Services;
public class TestDataService(LibraryContext libraryContext) : ITestDataService
{
    protected readonly LibraryContext _libraryContext = libraryContext;

    public async Task<bool> AddTestData()
    {
        if (await this._libraryContext.Books.CountAsync() >= 10)
        {
            return false;
        }

        List<Book> books = [];

        Randomizer.Seed = new Random(20240719);

        var bookId = 1;
        var bookFaker = new Faker<Book>()
           //.RuleFor(book => book.BookId, _ => bookId++)
           .RuleFor(book => book.Title, f => string.Join(" ", f.Random.Words(f.Random.Number(1, 3))))
           .RuleFor(book => book.Author, f => f.Name.FullName())
           .RuleFor(book => book.Description, f => f.Lorem.Sentence())
           .RuleFor(book => book.CoverImage, f => f.System.FilePath())
           .RuleFor(book => book.Publisher, f => f.Company.CompanyName())
           .RuleFor(book => book.PublicationDate, f => f.Date.PastDateOnly(f.Random.Number(5, 25)))
           .RuleFor(book => book.Category, f => f.Random.Word())
           .RuleFor(book => book.Isbn, f => f.Random.AlphaNumeric(20))
           .RuleFor(book => book.PageCount, f => f.Random.Number(100, 500))
           ;

        var bookData = bookFaker.Generate(10);

        books.AddRange(bookData);

        await this._libraryContext.AddRangeAsync(books);
        await this._libraryContext.SaveChangesAsync();

        return true;
    }
}

