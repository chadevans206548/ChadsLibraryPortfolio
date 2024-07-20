using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.Reviews;
using Microsoft.AspNetCore.Mvc;

namespace API.WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ReviewController(IReviewService reviewService) : ControllerBase
{
    private readonly IReviewService _reviewService = reviewService;

    [HttpPost]
    [Route("AddReview")]
    public async Task<ActionResult<ReviewViewModel>> AddReview([FromBody] AddReviewViewModel addReviewViewModel)
    {
        return await this._reviewService.AddReview(addReviewViewModel);
    }
}
