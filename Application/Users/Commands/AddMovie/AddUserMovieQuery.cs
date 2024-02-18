using Application.Movies.Dtos;
using Application.Users.Dtos;
using MediatR;

namespace Application.Users.Commands.AddMovie
{
    public class AddUserMovieQuery : IRequest<MovieDto>
    {
        public AddUserMovieQuery(AddMovieDto addMovieDto)
        {
            AddMovieDto = addMovieDto;
        }

        public AddMovieDto AddMovieDto { get; }
    }
}
