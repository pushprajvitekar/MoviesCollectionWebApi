using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Commands.RemoveMovie
{
    public class RemoveUserMovieQuery : IRequest<UserMovieDto>
    {
        public RemoveUserMovieQuery(string userId, RemoveMovieDto removeMovieDto)
        {
            UserId = userId;
            RemoveMovieDto = removeMovieDto;
        }

        public string UserId { get; }
        public RemoveMovieDto RemoveMovieDto { get; }
    }
}
