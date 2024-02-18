using Application.Movies.Dtos;
using Domain.Movies;
using Domain.Movies.Queries;
using MediatR;
using System.Net.Http;
using System.Security.Claims;

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
            if (request?.CreateMovieDto is CreateMovieDto createMovieDto)
            {
                //check name and category unique
                //var mv = movieRepository.GetAll(new MovieFilter { Name = createMovieDto.Name });
                //if (mv.Any(c => c.Name.Equals(createMovieDto.Name, StringComparison.CurrentCultureIgnoreCase))) { throw new DomainException("Movie already exists with Name {} ", null, DomainErrorCode.Exists); }
                //var principal = httpContext.HttpContext.User;
                //var createdBy = principal.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown";
                var movie = new Movie(createMovieDto.Name);
                var res = await movieRepository.Add(movie);
                return new MovieDto(res.Id, res.Name, res.Description, res.Genre.Name);
            }
            return null;
        }
    }
}
