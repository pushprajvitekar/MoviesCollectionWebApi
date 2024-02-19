using Application.Common;
using Domain.Movies;
using Domain.Movies.Queries;

namespace Application.Movies
{
    public interface IMovieRepository
    {
        Task<Movie> Add(Movie movie);

        Task<Movie> Update(Movie movie);
        Task<Movie?> GetById(int id);
        Task<Page<Movie>> GetAll(MovieFilter? filter = null, SortingPaging? sortingPaging = null);
        Task<IList<MovieGenre>> GetAllGenres();

    }
}
