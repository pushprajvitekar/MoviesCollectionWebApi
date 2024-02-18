namespace Domain.Movies
{
    public class MovieGenre
    {
        public MovieGenre() : this(MovieGenreEnum.Action)
        {
        }
        public MovieGenre(MovieGenreEnum genre)
        {
            Id = (int)genre;
            Name = genre.ToString();
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }
    }
}
