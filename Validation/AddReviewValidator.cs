using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.Reviews;
using FluentValidation;

namespace Validation;
public class AddReviewValidator : AbstractValidator<AddReviewViewModel>
{
    private readonly IBookService _bookService;
    public AddReviewValidator(IBookService bookService)
    {
        this._bookService = bookService;

        this.RuleFor(entity => entity.BookId).GreaterThan(0);
        this.RuleFor(entity => entity.Description).NotEmpty().MaximumLength(255);

        this.RuleFor(entity => entity.Rating)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(5)
            .WithMessage("Valid ratings are between 1 and 5.");

        this.RuleFor(entity => entity)
            .MustAsync(this.Exist)
            .WithMessage("Something went wrong. That book is not found.");
    }

    private async Task<bool> Exist(AddReviewViewModel vm, CancellationToken cancellationToken)
    {
        var results = await this._bookService.GetBook(vm.BookId);
        return results != null;
    }
}
