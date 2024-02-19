using Application.Users.Dtos;
using FluentValidation;

namespace Application.Users.Validators
{
    public class RemoveMovieDtoValidator : AbstractValidator<RemoveMovieDto>
    {
        public RemoveMovieDtoValidator()
        {
            RuleFor(a => a.MovieId).NotEmpty().GreaterThan(0);
        }
    }
}
