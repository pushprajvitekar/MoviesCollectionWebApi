using Application.Users.Dtos;
using FluentValidation;

namespace Application.Users.Validators
{
    public class AddMovieDtoValidator: AbstractValidator<AddMovieDto>
    {
        public AddMovieDtoValidator()
        {
            RuleFor(a=>a.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}
