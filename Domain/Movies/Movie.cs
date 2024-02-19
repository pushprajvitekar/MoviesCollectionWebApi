using Domain.Users;

namespace Domain.Movies
{
    public class Movie
    {
        public Movie(string name, int movieGenreId, string? description)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            MovieGenreId = movieGenreId;
            Description = description;
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public string? Description { get; protected set; }
        public MovieGenre MovieGenre { get; protected set; }
        public int MovieGenreId { get; protected set; }
        public ICollection<UserMovie> Users { get; set; } = new HashSet<UserMovie>();

        public void UpdateGenre(MovieGenre genre)
        {
            MovieGenre = genre;
        }
        public void UpdateMovieName(string name)
        {
            Name = name;
        }
        public void UpdateMovieDescription(string desc)
        {
            Description = desc;
        }
    }
}
