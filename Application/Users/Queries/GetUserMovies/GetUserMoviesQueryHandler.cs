using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Queries.GetUserMovies
{
    public class GetUserMoviesQueryHandler : IRequestHandler<GetUserMoviesQuery, IEnumerable<UserMovieDto>>
    {
        private readonly IUserMovieRepository movieRepository;

        public GetUserMoviesQueryHandler(IUserMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        public async Task<IEnumerable<UserMovieDto>> Handle(GetUserMoviesQuery request, CancellationToken cancellationToken)
        {
            var res = await movieRepository.GetAll(request.UserName, request.Filter, request.SortingPaging);

            if (res?.TotalItemCount > 0 && res.Items != null)
            {
                var models = res.Items.Select(c => new UserMovieDto(c.Id, c.Movie.Id, c.Movie.Name, c.Movie.Description, c.Movie.MovieGenre.Name));
                return models;
            }
            return [];
        }
    }
}
