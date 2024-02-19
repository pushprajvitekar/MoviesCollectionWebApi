using Application.Movies.Dtos;
using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Commands.AddMovie
{
    public class AddUserMovieQuery : IRequest<UserMovieDto>
    {
        public AddUserMovieQuery(string userId, AddMovieDto addMovieDto)
        {
            UserId = userId;
            AddMovieDto = addMovieDto;
        }

        public string UserId { get; }
        public AddMovieDto AddMovieDto { get; }
    }
}
