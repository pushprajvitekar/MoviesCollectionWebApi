using Application.Movies.Dtos;
using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Commands.RemoveMovie
{
    public class RemoveUserMovieQuery : IRequest<MovieDto>
    {
        public RemoveUserMovieQuery(RemoveMovieDto removeMovieDto)
        {
            RemoveMovieDto = removeMovieDto;
        }

        public RemoveMovieDto RemoveMovieDto { get; }
    }
}
