using Application.Common;
using Application.Movies;
using Application.Movies.Commands.CreateMovie;
using Application.Movies.Commands.UpdateMovie;
using Application.Movies.Dtos;
using Application.Movies.Queries.GetMovies;
using AutoFixture;
using Domain.Movies;
using Domain.Movies.Queries;
using Moq;
using System.Data;

namespace Application.Tests.MovieTests
{
    public class CommandTests
    {
        private Mock<IMovieRepository> _mockMovieRepository;
        private readonly Fixture _fixture;
        private readonly IEnumerable<Movie> _movies = [];
        public CommandTests()
        {
            // fixture for creating test data
            _fixture = new Fixture();

            // mock user repo dependency
            _mockMovieRepository = new Mock<IMovieRepository>();
            var moviesFixture = _fixture.Build<Movie>()
                                    .Do(b =>
                                    {
                                        var genre = _fixture.Create<MovieGenre>();
                                        b.UpdateGenre(genre);

                                    })
                                  .CreateMany(5);
            _movies = moviesFixture;
            var moviesPageFixture = _fixture.Create<Page<Movie>>();
            moviesPageFixture.Items = moviesFixture;

            var allGenres = Enum.GetValues<MovieGenreEnum>()
                                .Select(value => value)
                                .Select(type => new MovieGenre(type))
                                .ToList();

            _mockMovieRepository.Setup(x => x.GetAllGenres()).ReturnsAsync(allGenres);

        }

        [Fact]
        public async Task GetAll_ReturnsAllMovies()
        {
            // Arrange
            var handler = new GetMoviesQueryHandler(_mockMovieRepository.Object);
            _mockMovieRepository.Setup(x => x.GetAll(It.IsAny<MovieFilter?>(), It.IsAny<SortingPaging?>())).ReturnsAsync(new Page<Movie>() { Items = _movies, TotalItemCount = _movies.Count() });

            // Act
            var movieDtos = await handler.Handle(new GetMoviesQuery(It.IsAny<MovieFilter?>(), It.IsAny<SortingPaging?>()), CancellationToken.None);

            // Assert
            Assert.True(movieDtos.Count() == 5);
            Assert.Equal(_movies.First().Name, movieDtos.First().Name);
        }

        [Fact]
        public async Task CreateMovie_ReturnsMovie()
        {
            // Arrange
            var movieDtoFixture = _fixture.Create<CreateMovieDto>();
            var movie = _fixture.Build<Movie>().Do(m =>
            {
                m.UpdateMovieName(movieDtoFixture.Name);
                m.UpdateGenre(new MovieGenre(movieDtoFixture.MovieGenre));
                m.UpdateMovieDescription(movieDtoFixture.Description ?? string.Empty);
            }
            ).Create();
            _mockMovieRepository.Setup(x => x.GetAll(It.IsAny<MovieFilter?>(), It.IsAny<SortingPaging?>())).ReturnsAsync(new Page<Movie>() { Items = new List<Movie>(), TotalItemCount = 0 });
            _mockMovieRepository.Setup(x => x.Add(It.IsAny<Movie>())).ReturnsAsync(movie);


            // Act
            var handler = new CreateMovieRequestHandler(_mockMovieRepository.Object);
            var movieDto = await handler.Handle(new CreateMovieRequest(movieDtoFixture), CancellationToken.None);

            // Assert
            Assert.Equal(movieDtoFixture.Name, movieDto?.Name);
        }

        [Fact]
        public async Task UpdateMovie_ReturnsMovie()
        {
            // Arrange
            var movieDtoFixture = _fixture.Create<UpdateMovieDto>();
            int movieId = 1;
          
            var movie = _fixture.Build<Movie>().Do(m =>
            {
                m.UpdateMovieName("old name");
                m.UpdateGenre(new MovieGenre(MovieGenreEnum.Drama));
                m.UpdateMovieDescription("old description");
            }
            ).Create();

            var genres = _fixture.CreateMany<MovieGenre>(5);
            _mockMovieRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(movie);
            _mockMovieRepository.Setup(x => x.Update(It.IsAny<Movie>())).ReturnsAsync(movie);
            

            // Act
            var handler = new UpdateMovieRequestHandler(_mockMovieRepository.Object);
            var movieDto = await handler.Handle(new UpdateMovieRequest(movieId, movieDtoFixture), CancellationToken.None);

            // Assert
            Assert.Equal(movieDtoFixture.Name, movieDto?.Name);
        }
    }
}