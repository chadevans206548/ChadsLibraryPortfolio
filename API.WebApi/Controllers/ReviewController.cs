using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChadsLibraryPortfolio.Helpers;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.Books;
using ChadsLibraryPortfolio.ViewModels.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.WebApi.Controllers;

[Authorize(Policy = Constants.AuthPolicy.AuthenticatedUser)]
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
