using Domain.Movies;

namespace Domain.Users
{
    public class UserMovie
    {
        public int Id { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual UserMovieReview UserMovieReview { get; set; }

    }
}
