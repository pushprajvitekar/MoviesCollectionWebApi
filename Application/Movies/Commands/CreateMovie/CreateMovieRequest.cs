using Application.Movies.Dtos;
using MediatR;

namespace Application.Movies.Commands.CreateMovie
{
    public class CreateMovieRequest : IRequest<MovieDto?>
    {
        public CreateMovieRequest(CreateMovieDto createMovieDto)
        {
            CreateMovieDto = createMovieDto;
        }

        public CreateMovieDto CreateMovieDto { get; }
    }
}
