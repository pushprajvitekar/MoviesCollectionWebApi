using Domain.Movies;

namespace Domain.Users
{
    public class UserMovie
    {
        public int Id { get; protected set; }

        public ApplicationUser User { get; protected set; }
        public string AspNetUsersId { get; protected set; }
        public Movie Movie { get; protected set; }
        public int MovieId { get; protected set; }
        public UserMovieReview UserMovieReview { get; protected set; }

        public void AssociateUserMovie(ApplicationUser user, Movie movie)
        { 
            User = user;
            Movie = movie;
        }
    }
}
