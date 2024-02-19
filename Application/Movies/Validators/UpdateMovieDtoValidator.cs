using Application.Movies.Dtos;
using FluentValidation;

namespace Application.Movies.Validators
{
    public class UpdateMovieDtoValidator : AbstractValidator<UpdateMovieDto>
    {
        public UpdateMovieDtoValidator()
        {
            When(c => !string.IsNullOrEmpty(c.Name), () => RuleFor(c => c.Name).MaximumLength(400));
            When(c => !string.IsNullOrEmpty(c.Description), () => RuleFor(c => c.Description).MaximumLength(2000));
            When(c => c.MovieGenre != null, () => RuleFor(c => c.MovieGenre).IsInEnum());
        }
    }
}
