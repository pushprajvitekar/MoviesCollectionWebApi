using Application.Movies.Dtos;
using MediatR;

namespace Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieRequestHandler : IRequestHandler<UpdateMovieRequest, MovieDto>
    {
        public Task<MovieDto> Handle(UpdateMovieRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
