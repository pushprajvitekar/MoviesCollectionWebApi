using Application.Movies.Dtos;
using Domain.Common;
using Domain.Movies.Queries;
using MediatR;

namespace Application.Movies.Queries.GetMovies
{
    public class GetMoviesQuery: IRequest<IEnumerable<MovieDto>>
    {
        public GetMoviesQuery(MovieFilter? filter , SortingPaging? sortingPaging)
        {
            Filter = filter;
            SortingPaging = sortingPaging;
        }

        public MovieFilter? Filter { get; }
        public SortingPaging? SortingPaging { get; }
    }
}
