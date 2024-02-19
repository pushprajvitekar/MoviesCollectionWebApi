using Application.Common;
using Application.Users;
using Domain.Users;
using Domain.Users.Queries;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace EFCorePersistence
{
    public partial class MovieCollectionDBContext : IUserMovieRepository
    {
        public async Task<UserMovie> Add(UserMovie userMovie)
        {
            UserMovies.Attach(userMovie);
            await SaveChangesAsync();
            var res = await Movies.Include(m => m.MovieGenre).FirstAsync(m => m.Id == userMovie.Movie.Id);
            return userMovie;
        }

        public async Task<bool> Delete(UserMovie userMovie)
        {
            UserMovies.Remove(userMovie);
            await SaveChangesAsync();
            return true;
        }

        public async Task<Page<UserMovie>> GetAll(string username, UserMovieFilter? filter = null, SortingPaging? sortingPaging = null)
        {
            var pred = PredicateBuilder.New<UserMovie>(true);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.MovieName))
                {
                    pred = pred.And(a => a.Movie.Name.Contains(filter.MovieName));
                }
                if (filter.GenreId != null)
                {
                    var genreId = filter.GenreId.GetValueOrDefault();
                    pred = pred.And(a => a.Movie.MovieGenreId == genreId);
                }
            }
            var sortPage = sortingPaging ?? new SortingPaging();
            string sortBy = sortPage.SortBy ?? "Id";

            var usermovies = await UserMovies

                                    .Include(u => u.User)
                                    .Include(u => u.Movie)
                                    .Include(u => u.Movie.MovieGenre)
                                    .Where(u =>  u.User.NormalizedUserName == username.ToUpper())
                                    .AsExpandable()
                                    .Where(pred)
                                    .ToPagedAsync(sortPage.PageNumber,sortPage.PageSize,sortBy,sortPage.SortAsc);

            return usermovies;
        }


        public async Task<UserMovie?> GetByUserName(string username, int movieId)
        {
            var usermovie = await UserMovies

                                   .Include(u => u.User)
                                   .Include(u => u.Movie)
                                   .Include(u => u.Movie.MovieGenre)
                                   .Where(u => u.User.Id == username && u.Movie.Id == movieId)
                                   .FirstOrDefaultAsync();
            return usermovie;

        }
    }
}
