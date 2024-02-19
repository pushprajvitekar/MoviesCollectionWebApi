using Application.Movies.Dtos;
using Domain.Exceptions;
using Domain.Movies;
using Domain.Movies.Queries;
using MediatR;

namespace Application.Movies.Commands.CreateMovie
{
    public class CreateMovieRequestHandler : IRequestHandler<CreateMovieRequest, MovieDto?>
    {
        private readonly IMovieRepository movieRepository;

        public CreateMovieRequestHandler(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }
        public async Task<MovieDto?> Handle(CreateMovieRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var createMovieDto = request.CreateMovieDto;
            //check name 
            var mv = await movieRepository.GetAll(new MovieFilter { Name = createMovieDto.Name });
            if (mv?.Items.Any(c => c.Name.Equals(createMovieDto.Name, StringComparison.CurrentCultureIgnoreCase)) == true)
            {
                throw new DomainException($"Movie already exists with Name {createMovieDto.Name} ", null, DomainErrorCode.Exists);
            }
            var movie = new Movie(createMovieDto.Name, (int)createMovieDto.MovieGenre, createMovieDto.Description);
            var res = await movieRepository.Add(movie);
            return new MovieDto(res.Id, res.Name, res.Description, res.MovieGenre.Name);
        }
    }
}
