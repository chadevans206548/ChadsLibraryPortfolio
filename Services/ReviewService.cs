using AutoMapper;
using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.Model.Entities;
using ChadsLibraryPortfolio.Models;
using ChadsLibraryPortfolio.ViewModels.Reviews;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Validation;
using ViewModels.Common;

namespace ChadsLibraryPortfolio.Services;
public class ReviewService(LibraryContext libraryContext, IMapper mapper, IBookService bookService) : IReviewService
{
    protected readonly LibraryContext _libraryContext = libraryContext;
    protected readonly IMapper _mapper = mapper;
    protected readonly IBookService _bookService = bookService;
    public async Task<ReviewViewModel> AddReview(AddReviewViewModel addReviewViewModel)
    {
        AddReviewValidator validator = new AddReviewValidator(_bookService);
        ValidationResult result = await validator.ValidateAsync(addReviewViewModel);

        if (result.IsValid)
        {
            var review = this._mapper.Map<Review>(addReviewViewModel);
            var entity = await this._libraryContext.AddAsync(review);
            await this._libraryContext.SaveChangesAsync();
            return this._mapper.Map<ReviewViewModel>(entity.Entity);
        }
        else
        {
            return new ReviewViewModel();
        }
    }

    public async Task<ValidationResultViewModel> ValidateAddReview(AddReviewViewModel addReviewViewModel)
    {
        AddReviewValidator validator = new AddReviewValidator(_bookService);
        ValidationResult result = await validator.ValidateAsync(addReviewViewModel);
        return this._mapper.Map<ValidationResultViewModel>(result);
    }

    public async Task<List<ReviewViewModel>> GetReviewsByBook(int bookId)
    {
        var entity = await this._libraryContext.Reviews.Where(x => x.BookId == bookId).ToListAsync();
        return this._mapper.Map<List<ReviewViewModel>>(entity);
    }
}
