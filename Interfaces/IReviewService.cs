using ChadsLibraryPortfolio.ViewModels.InventoryLogs;
using ChadsLibraryPortfolio.ViewModels.Reviews;
using ViewModels.Common;

namespace ChadsLibraryPortfolio.Interfaces;
public interface IReviewService
{
    Task<ReviewViewModel> AddReview(AddReviewViewModel addReviewViewModel);
    Task<ValidationResultViewModel> ValidateAddReview(AddReviewViewModel addReviewViewModel);
}
