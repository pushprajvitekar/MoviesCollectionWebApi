using Domain.Movies;

namespace Domain.Users.Queries
{
    public class UserMovieFilter
    {
        public string? MovieName { get; set; }
        public MovieGenreEnum? Genre { get; set; }
    }
}
