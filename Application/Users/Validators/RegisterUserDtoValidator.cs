using Application.Users.Dtos;
using FluentValidation;

namespace Application.Users.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(a => a.Email).NotEmpty().EmailAddress();
            RuleFor(a => a.Username).NotEmpty().MinimumLength(2).MaximumLength(20);
            RuleFor(a => a.Password).NotEmpty().MinimumLength(6).MaximumLength(20);
        }
    }
}
