using AutoMapper;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Model.Entities;
using ChadsLibraryPortfolio.Models;
using ChadsLibraryPortfolio.ViewModels.Reviews;

namespace ChadsLibraryPortfolio.Services;
public class ReviewService(LibraryContext libraryContext, IMapper mapper) : IReviewService
{
    protected readonly LibraryContext _libraryContext = libraryContext;
    protected readonly IMapper _mapper = mapper;
    public async Task<ReviewViewModel> AddReview(AddReviewViewModel addReviewViewModel)
    {
        var review = this._mapper.Map<Review>(addReviewViewModel);
        var entity = await this._libraryContext.AddAsync(review);
        await this._libraryContext.SaveChangesAsync();
        return this._mapper.Map<ReviewViewModel>(entity.Entity);
    }
}
