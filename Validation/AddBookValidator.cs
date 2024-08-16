using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.Books;
using FluentValidation;

namespace Validation;
public class AddBookValidator : AbstractValidator<AddBookViewModel>
{
    private readonly IBookService _bookService;
    public AddBookValidator(IBookService bookService)
    {
        this._bookService = bookService;

        this.RuleFor(entity => entity.Title).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Author).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Description).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.CoverImage).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Publisher).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Category).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Isbn).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.PageCount).GreaterThan(0);

        this.RuleFor(entity => entity)
            .MustAsync(this.NotDuplicateTitle)
            .WithMessage("The book title must be unique.");

        this.RuleFor(entity => entity)
            .MustAsync(this.NotDuplicateIsbn)
            .WithMessage("The book ISBN must be unique.");
    }

    private async Task<bool> NotDuplicateTitle(AddBookViewModel vm, CancellationToken cancellationToken)
    {
        var results = await this._bookService.GetBookByTitle(vm.Title);
        return results == null;
    }

    private async Task<bool> NotDuplicateIsbn(AddBookViewModel vm, CancellationToken cancellationToken)
    {
        var results = await this._bookService.GetBookByIsbn(vm.Isbn);
        return results == null;
    }
}
