using Application.Movies.Dtos;
using Domain.Exceptions;
using Domain.Movies.Queries;
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
        public async Task<MovieDto> Handle(UpdateMovieRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var updateMovieDto = request.UpdateMovieDto;
            //check name 
            var mv = await movieRepository.GetById(request.MovieId);
            if (mv == null)
            {
                throw new DomainException($"Movie not found ", null, DomainErrorCode.NotFound);
            }
            if (updateMovieDto?.MovieGenre != null)
            {
                var genreId = (int)updateMovieDto.MovieGenre.GetValueOrDefault();
                if (mv.MovieGenre.Id != genreId)
                {
                    var genres = await movieRepository.GetAllGenres();
                    var genre = genres.FirstOrDefault(g => g.Id == genreId);
                    if (genre != null)
                    {
                        mv.UpdateGenre(genre);
                    }
                }
            }
            if (!string.IsNullOrEmpty(updateMovieDto?.Name) && !string.Equals(mv.Name, updateMovieDto.Name, StringComparison.InvariantCulture))
            {
                mv.UpdateMovieName(updateMovieDto.Name);
            }
            if (!string.IsNullOrEmpty(updateMovieDto?.Description) && !string.Equals(mv.Description, updateMovieDto.Description, StringComparison.InvariantCulture))
            {
                mv.UpdateMovieDescription(updateMovieDto.Description);
            }
            var updateMv = await movieRepository.Update(mv);
            return new MovieDto(updateMv.Id, updateMv.Name, updateMv.Description, updateMv.MovieGenre.Name);
        }
    }
}
