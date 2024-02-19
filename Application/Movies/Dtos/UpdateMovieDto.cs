using Domain.Movies;

namespace Application.Movies.Dtos
{
    public class UpdateMovieDto
    {
        public  string? Name { get; set; }
        public string? Description { get; set; }
        public MovieGenreEnum? MovieGenre { get; set; }
    }
}
