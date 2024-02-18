using Application.Movies.Dtos;
using MediatR;

namespace Application.Users.Queries.GetUserMovies
{
    public class GetUserMoviesQueryHandler : IRequestHandler<GetUserMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IUserMovieRepository movieRepository;

        public GetUserMoviesQueryHandler(IUserMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public Task<IEnumerable<MovieDto>> Handle(GetUserMoviesQuery request, CancellationToken cancellationToken)
        {
            var res = movieRepository.GetAll  (1, request.Filter, request.SortingPaging)
                                   .Select(c => new MovieDto(c.Id, c.Movie.Name, c.Movie.Description, c.Movie.Genre.Name));

            return Task.FromResult(res);
        }
    }
}
