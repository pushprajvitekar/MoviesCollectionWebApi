using Application.Common;
using Application.Movies;
using Application.Movies.Commands.CreateMovie;
using Application.Movies.Dtos;
using Application.Movies.Queries.GetMovies;
using AutoFixture;
using Domain.Movies;
using Domain.Movies.Queries;
using Moq;

namespace WebApiTests.MovieTests
{
    public class CommandTests
    {
        private Mock<IMovieRepository> _mockMovieRepository;
        private Fixture _fixture;
        public CommandTests()
        {
            // fixture for creating test data
            _fixture = new Fixture();

            // mock user repo dependency
            _mockMovieRepository = new Mock<IMovieRepository>();
        }

        [Fact]
        public async Task GetAll_ReturnsAllMovies()
        {
            // Arrange
            var handler = new GetMoviesQueryHandler(_mockMovieRepository.Object);
            var moviesFixture = _fixture.Build<Movie>()
                                        .Do(b =>
                                        {
                                            var genre = _fixture.Create<MovieGenre>();
                                            b.UpdateGenre(genre);

                                        })
                                      .CreateMany(5);
            var moviesPageFixture = _fixture.Create<Page<Movie>>();
            moviesPageFixture.Items = moviesFixture;

            var sorting = new SortingPaging();
            var filter = new MovieFilter();
            _mockMovieRepository.Setup(x => x.GetAll(filter, sorting)).ReturnsAsync(moviesPageFixture);

            // Act
            var movieDtos = await handler.Handle(new GetMoviesQuery(filter, sorting), CancellationToken.None);

            // Assert
            Assert.True(movieDtos.Count() == 5);
            Assert.Equal(moviesPageFixture.Items.First().Name, movieDtos.First().Name);
        }

        [Fact]
        public async Task CreateMovie_ReturnsMovie()
        {
            // Arrange
            var movieDtoFixture = _fixture.Create<CreateMovieDto>();
            var movie = new Movie(movieDtoFixture.Name, (int)movieDtoFixture.MovieGenre, movieDtoFixture.Description);
            movie.UpdateGenre(new MovieGenre(MovieGenreEnum.Action));
            var moviesPageFixture = _fixture.Create<Page<Movie>>();
            moviesPageFixture.Items = new List<Movie>();
            
            var sorting = _fixture.Create<SortingPaging>();
            var filter = _fixture.Create<MovieFilter>();
            _mockMovieRepository.Setup(x => x.GetAll(filter, sorting)).ReturnsAsync(moviesPageFixture);
            _mockMovieRepository.Setup(x => x.Add(movie)).ReturnsAsync(movie);
            
       
            // Act
            var handler = new CreateMovieRequestHandler(_mockMovieRepository.Object);
            var movieDto = await handler.Handle(new CreateMovieRequest(movieDtoFixture), CancellationToken.None);

            // Assert
            Assert.Equal(movieDtoFixture.Name, movieDto?.Name);
        }
    }
}