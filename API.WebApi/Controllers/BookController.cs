using ChadsLibraryPortfolio.Helpers;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Common;

namespace API.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class BookController(IBookService bookService) : ControllerBase
{
    private readonly IBookService _bookService = bookService;

    [HttpGet]
    [Route("GetBook/{bookId}")]
    public async Task<ActionResult<BookViewModel>> GetBook(int bookId)
    {
        return await this._bookService.GetBook(bookId);
    }

    [HttpGet]
    [Route("GetFeaturedBooks")]
    public async Task<ActionResult<List<BookViewModel>>> GetFeaturedBooks()
    {
        return await this._bookService.GetFeaturedBooks();
    }

    [HttpGet]
    [Route("GetAllBooks")]
    public async Task<ActionResult<List<BookViewModel>>> GetAllBooks()
    {
        return await this._bookService.GetAllBooks();
    }

    [HttpGet]
    [Route("GetAllBookTitles")]
    public async Task<ActionResult<List<string>>> GetAllBookTitles()
    {
        return await this._bookService.GetAllBookTitles();
    }

    [HttpPost]
    [Authorize(Roles = Constants.AuthPolicy.LibrarianUser)]
    [Route("AddBook")]
    public async Task<ActionResult<BookViewModel>> AddBook([FromBody] AddBookViewModel addBookViewModel)
    {
        return await this._bookService.AddBook(addBookViewModel);
    }

    [HttpPost]
    [Authorize(Roles = Constants.AuthPolicy.LibrarianUser)]
    [Route("ValidateAddBook")]
    public async Task<ValidationResultViewModel> ValidateAddBook([FromBody] AddBookViewModel addBookViewModel)
    {
        return await this._bookService.ValidateAddBook(addBookViewModel);
    }

    [HttpDelete]
    [Authorize(Roles = Constants.AuthPolicy.LibrarianUser)]
    [Route("DeleteBook/{bookId}")]
    public async Task<ActionResult<bool>> DeleteBook(int bookId)
    {
        return await this._bookService.DeleteBook(bookId);
    }

    [HttpPut]
    [Authorize(Roles = Constants.AuthPolicy.LibrarianUser)]
    [Route("EditBook")]
    public async Task<ActionResult<BookViewModel>> EditBook([FromBody] EditBookViewModel editBookViewModel)
    {
        return await this._bookService.EditBook(editBookViewModel);
    }

    [HttpPut]
    [Authorize(Roles = Constants.AuthPolicy.LibrarianUser)]
    [Route("ValidateEditBook")]
    public async Task<ValidationResultViewModel> ValidateEditBook([FromBody] EditBookViewModel editBookViewModel)
    {
        return await this._bookService.ValidateEditBook(editBookViewModel);
    }
}
