using ChadsLibraryPortfolio.Interfaces;
using ChadsLibraryPortfolio.ViewModels.Books;
using FluentValidation;

namespace ViewModels.Books.Validators;
public class EditBookValidator : AbstractValidator<EditBookViewModel>
{
    private readonly IBookService _bookService;
    public EditBookValidator(IBookService bookService)
    {
        this._bookService = bookService;

        this.RuleFor(entity => entity)
            .MustAsync(this.Exist)
            .WithMessage("Something went wrong. That book is not found.");

        this.RuleFor(entity => entity.Title).NotEmpty().MaximumLength(255);

        this.RuleFor(entity => entity)
            .MustAsync(this.NotDuplicateTitle)
            .WithMessage("The book title must be unique.");

        this.RuleFor(entity => entity.Author).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Description).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.CoverImage).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Publisher).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Category).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Isbn).NotEmpty().MaximumLength(255);

        this.RuleFor(entity => entity)
            .MustAsync(this.NotDuplicateIsbn)
            .WithMessage("The book ISBN must be unique.");

        this.RuleFor(entity => entity.PageCount).GreaterThan(0);
    }

    private async Task<bool> Exist(EditBookViewModel vm, CancellationToken cancellationToken)
    {
        var results = await this._bookService.GetBook(vm.BookId);
        return results != null;
    }

    private async Task<bool> NotDuplicateTitle(EditBookViewModel vm, CancellationToken cancellationToken)
    {
        var results = await this._bookService.GetBookByTitle(vm.Title);
        return results != null;
    }

    private async Task<bool> NotDuplicateIsbn(EditBookViewModel vm, CancellationToken cancellationToken)
    {
        var results = await this._bookService.GetBookByIsbn(vm.Isbn);
        return results != null;
    }
}
