using ChadsLibraryPortfolio.Helpers;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Common;

namespace API.WebApi.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;

    [HttpPost]
    [Authorize(Roles = Constants.AuthPolicy.CustomerUser)]
    [Route("AddReview")]
    public async Task<ActionResult<ReviewViewModel>> AddReview([FromBody] AddReviewViewModel addReviewViewModel)
    {
        return await this._reviewService.AddReview(addReviewViewModel);
    }

    [HttpPost]
    [Authorize(Roles = Constants.AuthPolicy.CustomerUser)]
    [Route("ValidateAddReview")]
    public async Task<ValidationResultViewModel> ValidateAddReview([FromBody] AddReviewViewModel addReviewViewModel)
    {
        return await this._reviewService.ValidateAddReview(addReviewViewModel);
    }

    [HttpGet]
    [Route("GetReviewsByBook/{bookId}")]
    public async Task<List<ReviewViewModel>> GetReviewsByBook(int bookId)
    {
        return await this._reviewService.GetReviewsByBook(bookId);
    }
}
