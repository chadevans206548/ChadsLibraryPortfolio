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
    [Route("SearchBooks/{titleSearch}")]
    public async Task<ActionResult<List<BookViewModel>>> SearchBooks(string titleSearch)
    {
        return await this._bookService.SearchBooks(titleSearch);
    }

    [HttpPost]
    [Route("AddBook")]
    public async Task<BookViewModel> AddBook([FromBody] AddBookViewModel addBookViewModel)
    {
        return await this._bookService.AddBook(addBookViewModel);
    }

    [HttpDelete]
    [Route("DeleteBook/{bookId}")]
    public async Task<bool> DeleteBook(int bookId)
    {
        return await this._bookService.DeleteBook(bookId);
    }

    [HttpPut]
    [Route("EditBook")]
    public async Task<BookViewModel> EditBook([FromBody] EditBookViewModel editBookViewModel)
    {
        return await this._bookService.EditBook(editBookViewModel);
    }
}
