using ChadsLibraryPortfolio.Helpers;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.WebApi.Controllers;

[Authorize(Policy = Constants.AuthPolicy.AuthenticatedUser)]
[Route("api/[controller]")]
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
    [Route("GetAllBookTitles")]
    public async Task<ActionResult<List<string>>> GetAllBookTitles()
    {
        return await this._bookService.GetAllBookTitles();
    }

    [HttpPost]
    [Route("AddBook")]
    public async Task<ActionResult<BookViewModel>> AddBook([FromBody] AddBookViewModel addBookViewModel)
    {
        return await this._bookService.AddBook(addBookViewModel);
    }

    [HttpDelete]
    [Route("DeleteBook/{bookId}")]
    public async Task<ActionResult<bool>> DeleteBook(int bookId)
    {
        return await this._bookService.DeleteBook(bookId);
    }

    [HttpPut]
    [Route("EditBook")]
    public async Task<ActionResult<BookViewModel>> EditBook([FromBody] EditBookViewModel editBookViewModel)
    {
        return await this._bookService.EditBook(editBookViewModel);
    }
}
