using Domain.Common;
using Domain.Movies;
using Domain.Movies.Queries;

namespace Application.Movies
{
    public interface IMovieRepository
    {
        Movie Add(Movie movie);

        Movie Update(Movie movie);
        Movie? GetById(int id);
        IList<Movie> GetAll(MovieFilter? filter = null, SortingPaging? sortingPaging = null);

    }
}
