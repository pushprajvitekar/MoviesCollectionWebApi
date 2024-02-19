using Application.Users.Dtos;
using FluentValidation;

namespace Application.Users.Validators
{
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            RuleFor(a => a.UserName).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(a => a.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
        }
    }
}
