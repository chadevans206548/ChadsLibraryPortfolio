using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.InventoryLogs;
using FluentValidation;

namespace Validation;
public class AddInventoryLogValidator : AbstractValidator<AddInventoryLogViewModel>
{
    private readonly IBookService _bookService;
    public AddInventoryLogValidator(IBookService bookService)
    {
        this._bookService = bookService;

        this.RuleFor(entity => entity.BookId).GreaterThan(0);
        this.RuleFor(entity => entity.User).NotEmpty().MaximumLength(255);

        this.RuleFor(entity => entity.CheckinDate)
            .GreaterThanOrEqualTo(entity => entity.CheckoutDate)
            .When(entity => entity.CheckoutDate.HasValue && entity.CheckinDate.HasValue)
            .WithMessage("Checkin date must occur after the checkout date.");

        this.RuleFor(entity => entity.DueDate)
            .Equal(entity => entity.CheckoutDate.Value.AddDays(5))
            .When(entity => entity.CheckoutDate.HasValue)
            .WithMessage("Due date must be 5 days past the checkout date.");

        this.RuleFor(entity => entity)
            .MustAsync(this.Exist)
            .WithMessage("Something went wrong. That book is not found.");
    }

    private async Task<bool> Exist(AddInventoryLogViewModel vm, CancellationToken cancellationToken)
    {
        var results = await this._bookService.GetBook(vm.BookId);
        return results != null;
    }
}
