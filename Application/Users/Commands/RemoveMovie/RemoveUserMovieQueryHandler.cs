using Application.Movies.Dtos;
using MediatR;

namespace Application.Users.Commands.RemoveMovie
{
    public class RemoveUserMovieQueryHandler : IRequestHandler<RemoveUserMovieQuery, MovieDto>
    {
        public Task<MovieDto> Handle(RemoveUserMovieQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
