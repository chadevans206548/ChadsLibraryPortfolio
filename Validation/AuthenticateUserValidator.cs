using FluentValidation;
using ViewModels.Authentication;

namespace Validation;
public class AuthenticateUserValidator : AbstractValidator<AuthenticateUserViewModel>
{
    public AuthenticateUserValidator()
    {
        this.RuleFor(entity => entity.Email).NotEmpty().MaximumLength(255);
        this.RuleFor(entity => entity.Password).NotEmpty().MaximumLength(255);
    }
}
