using Application.Movies.Dtos;
using Application.Common;
using Domain.Users.Queries;
using MediatR;
using Application.Users.Dtos;

namespace Application.Users.Queries.GetUserMovies
{
    public class GetUserMoviesQuery : IRequest<IEnumerable<UserMovieDto>>
    {
        public GetUserMoviesQuery(string userName, UserMovieFilter? filter, SortingPaging? sortingPaging)
        {
            UserName = userName;
            Filter = filter;
            SortingPaging = sortingPaging;
        }

        public string UserName { get; }
        public UserMovieFilter? Filter { get; }
        public SortingPaging? SortingPaging { get; }
    }
}
