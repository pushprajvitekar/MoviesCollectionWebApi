using Application.Movies.Dtos;
using MediatR;

namespace Application.Movies.Commands.CreateMovie
{
    public class CreateMovieRequestHandler : IRequestHandler<CreateMovieRequest, MovieDto>
    {
        public Task<MovieDto> Handle(CreateMovieRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
