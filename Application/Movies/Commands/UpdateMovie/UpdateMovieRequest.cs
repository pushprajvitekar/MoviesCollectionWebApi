using Application.Movies.Dtos;
using MediatR;

namespace Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieRequest : IRequest<MovieDto>
    {
        public UpdateMovieRequest(int movieId, UpdateMovieDto updateMovieDto)
        {
            MovieId = movieId;
            UpdateMovieDto = updateMovieDto;
        }

        public int MovieId { get; }
        public UpdateMovieDto UpdateMovieDto { get; }
    }
}
