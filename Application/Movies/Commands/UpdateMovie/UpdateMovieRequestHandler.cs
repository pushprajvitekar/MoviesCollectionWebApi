using Application.Movies.Dtos;
using MediatR;

namespace Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieRequestHandler : IRequestHandler<UpdateMovieRequest, MovieDto>
    {
        private readonly IMovieRepository movieRepository;

        public UpdateMovieRequestHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        public Task<MovieDto> Handle(UpdateMovieRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
