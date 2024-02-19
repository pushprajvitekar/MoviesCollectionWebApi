using Application.Users.Dtos;
using Domain.Exceptions;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Users.Queries.GetUserMovies
{
    public class GetUserMoviesQueryHandler : IRequestHandler<GetUserMoviesQuery, IEnumerable<UserMovieDto>>
    {
        private readonly IUserMovieRepository movieRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public GetUserMoviesQueryHandler(IUserMovieRepository movieRepository, UserManager<ApplicationUser> userManager)
        {
            this.movieRepository = movieRepository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<UserMovieDto>> Handle(GetUserMoviesQuery request, CancellationToken cancellationToken)
        {
            var user = userManager.Users.FirstOrDefault(u => u.UserName == request.UserName);
            if (user == null) { throw new DomainException("User not found", null, DomainErrorCode.NotFound); }
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
