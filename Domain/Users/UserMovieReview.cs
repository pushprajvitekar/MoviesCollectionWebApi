using Domain.Movies;

namespace Domain.Users
{
    public class UserMovieReview
    {
        public int Id { get; protected set; }
        public string Review { get; protected set; }
        public float Rating { get; protected set; }

        public int UserMovieId { get; set; }
        public UserMovie UserMovie { get; protected set; }

    }
}
