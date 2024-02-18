using Application.Movies.Dtos;
using MediatR;

namespace Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieRequest : IRequest<MovieDto>
    {
        public UpdateMovieRequest(UpdateMovieDto updateMovieDto)
        {
            UpdateMovieDto = updateMovieDto;
        }

        public UpdateMovieDto UpdateMovieDto { get; }
    }
}
