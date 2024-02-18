using Application.Movies;
using Application.Movies.Dtos;
using MediatR;

namespace Application.Movies.Queries.GetMovies
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IMovieRepository movieRepository;

        public GetMoviesQueryHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public Task<IEnumerable<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            var res = movieRepository.GetAll(request.Filter, request.SortingPaging)
                                   .Select(c => new MovieDto(c.Id, c.Name, c.Description, c.Genre.Name));

            return Task.FromResult(res);
        }
    }
}
