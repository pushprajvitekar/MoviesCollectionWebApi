using Application.Movies.Dtos;
using FluentValidation;

namespace Application.Movies.Validators
{
    public class CreateMovieDtoValidator : AbstractValidator<CreateMovieDto>
    {
        public CreateMovieDtoValidator()
        {
            RuleFor(s => s.Name).NotEmpty().MaximumLength(400);
            RuleFor(s => s.Description).MaximumLength(2000);
            RuleFor(s => s.MovieGenre).NotEmpty().IsInEnum();
        }
    }
}
