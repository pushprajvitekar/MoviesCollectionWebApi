using Application.Common;
using Application.Movies;
using Domain.Movies;
using Domain.Movies.Queries;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace EFCorePersistence
{
    public partial class MovieCollectionDBContext : IMovieRepository
    {
        public async Task<Movie> Add(Movie movie)
        {
            Movies.Add(movie);
            await SaveChangesAsync();
            var res = await Movies.Include(m => m.MovieGenre).FirstAsync(m => m.Id == movie.Id);
            return res;
        }

        public async Task<Page<Movie>> GetAll(MovieFilter? filter = null, SortingPaging? sortingPaging = null)
        {
            var pred = PredicateBuilder.New<Movie>(true);

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    pred = pred.And(a => a.Name.Contains(filter.Name));
                }
                if (filter.Genre != null)
                {
                    var genreId = (int)filter.Genre.GetValueOrDefault();
                    pred = pred.And(a => a.MovieGenre.Id == genreId);
                }
            }
            var sortPage = sortingPaging ?? new SortingPaging();
            var sortBy = sortPage.SortBy ?? "Id";
            var res = await Movies
                            .Include(m => m.MovieGenre)
                            .AsExpandable()
                                  .Where(pred)
                                  .ToPagedAsync(sortPage.PageNumber, sortPage.PageSize, sortBy, sortPage.SortAsc);


            return res;
        }

        public async Task<IList<MovieGenre>> GetAllGenres()
        {
            return await MovieGenres.ToListAsync();
        }

        public async Task<Movie?> GetById(int id)
        {
            var res = await Movies.Include(m => m.MovieGenre).FirstOrDefaultAsync(a => a.Id == id);
            return res;
        }

        public async Task<Movie> Update(Movie movie)
        {
            var res = await Movies.Include(m => m.MovieGenre).FirstOrDefaultAsync(a => a.Id == movie.Id);
            res = movie;
            await SaveChangesAsync();
            return movie;
        }
    }
}
