using Application.Movies;
using Domain.Common;
using Domain.Movies;
using Domain.Movies.Queries;
using Microsoft.EntityFrameworkCore;

namespace EFCorePersistence
{
    public partial class MovieCollectionDBContext : IMovieRepository
    {
        public async Task<Movie> Add(Movie movie)
        {
            Movies.Add(movie);
            await SaveChangesAsync();
            return movie;
        }

        public async Task<IList<Movie>> GetAll(MovieFilter? filter = null, SortingPaging? sortingPaging = null)
        {
            var res = await Movies.Where(a => a.Id == 1).ToArrayAsync();
            return res;
        }

        public async Task<Movie?> GetById(int id)
        {
            var res = await Movies.FirstOrDefaultAsync(a => a.Id == id);
            return res;
        }

        public async Task<Movie> Update(Movie movie)
        {
            var res = await Movies.FirstOrDefaultAsync(a => a.Id == movie.Id);
            res = movie;
            await SaveChangesAsync();
            return movie;
        }
    }
}
