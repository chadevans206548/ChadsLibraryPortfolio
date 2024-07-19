using ChadsLibraryPortfolio.ViewModels.Reviews;

namespace ChadsLibraryPortfolio.Interfaces;
public interface IReviewService
{
    Task<ReviewViewModel> AddReview(AddReviewViewModel addReviewViewModel);
}
