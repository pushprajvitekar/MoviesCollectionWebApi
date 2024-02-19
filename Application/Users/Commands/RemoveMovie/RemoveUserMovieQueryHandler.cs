using Application.Users.Dtos;
using Domain.Exceptions;
using MediatR;

namespace Application.Users.Commands.RemoveMovie
{
    public class RemoveUserMovieQueryHandler : IRequestHandler<RemoveUserMovieQuery, UserMovieDto>
    {
        private readonly IUserMovieRepository userMovieRepository;

        public RemoveUserMovieQueryHandler(IUserMovieRepository userMovieRepository)
        {
            this.userMovieRepository = userMovieRepository;
        }
        public async Task<UserMovieDto> Handle(RemoveUserMovieQuery request, CancellationToken cancellationToken)
        {
            var movieId = request.RemoveMovieDto.MovieId;
            var exists = await userMovieRepository.GetByUserName(request.UserId, movieId) ?? throw new DomainException("Movie not found", null, DomainErrorCode.NotFound);

            var res = await userMovieRepository.Delete(exists);
            if (res)
            {
                return new UserMovieDto(exists.Id, movieId, exists.Movie.Name, exists.Movie.Description, exists.Movie.MovieGenre.Name);
            }
            throw new DomainException("Unable to remove movie", null, DomainErrorCode.InfrastructureError);
        }
    }
}
