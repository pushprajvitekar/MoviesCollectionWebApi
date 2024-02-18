using Domain.Users;

namespace Domain.Movies
{
    public class Movie
    {
        public Movie(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }

        public string? Description { get; protected set; }
        public MovieGenre Genre { get; protected set; } = new MovieGenre(MovieGenreEnum.Action);
        public virtual ICollection<UserMovie> Users { get; set; }=new HashSet<UserMovie>();

    }
}
