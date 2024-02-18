using Domain.Movies;

namespace Application.Movies.Dtos
{
    public record CreateMovieDto(string Name, string? Description, MovieGenreEnum movieGenre);
}
