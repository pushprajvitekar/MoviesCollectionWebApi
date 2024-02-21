using Application.Common;
using Application.Movies;
using Application.Users;
using Application.Users.Commands.AddMovie;
using Application.Users.Commands.RemoveMovie;
using Application.Users.Dtos;
using Application.Users.Queries.GetUserMovies;
using AutoFixture;
using Domain.Exceptions;
using Domain.Movies;
using Domain.Users;
using Domain.Users.Queries;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Application.Tests.UserTests
{
    public class CommandTests
    {
        private Mock<IMovieRepository> _mockMovieRepository;
        private Mock<IUserMovieRepository> _mockUserMovieRepository;

        private Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Fixture _fixture;
        private readonly IEnumerable<Movie> _movies = [];
        private readonly IEnumerable<UserMovie> _usermovies = [];

        Mock<RoleManager<IdentityRole>> _roleManagerMock;
        public CommandTests()
        {
            // fixture for creating test data
            _fixture = new Fixture();

            // mock user repo dependency
            _mockMovieRepository = new Mock<IMovieRepository>();
            _mockUserMovieRepository = new Mock<IUserMovieRepository>();
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

            var usermoviesFixture = _fixture.Build<UserMovie>();
            _usermovies = _movies
                                    .Select(m =>
                                    usermoviesFixture
                                      .Do(b =>
                                    {
                                        b.AssociateUserMovie(It.IsAny<ApplicationUser>(), m);

                                    })
                                  .Create());
            var users = new List<Tuple<string,string>> { new("1","user1"), new("2","user2"), new("3","user3") };
            var usersFixture = users.Select(u =>
                _fixture.Build<ApplicationUser>()
                              .With(b => b.UserName, u.Item2)
                              .With(b=>b.Id,u.Item1)

                            .Create());

            _mockUserManager = new Mock<UserManager<ApplicationUser>>(new Mock<IUserStore<ApplicationUser>>().Object,
                                                                      new Mock<IOptions<IdentityOptions>>().Object,
                                                                      new Mock<IPasswordHasher<ApplicationUser>>().Object,
                                                                      new IUserValidator<ApplicationUser>[0],
                                                                      new IPasswordValidator<ApplicationUser>[0],
                                                                      new Mock<ILookupNormalizer>().Object,
                                                                      new Mock<IdentityErrorDescriber>().Object,
                                                                      new Mock<IServiceProvider>().Object,
                                                                      new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

            _mockUserManager
                .Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success));
            _mockUserManager
                .Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
            _mockUserManager
               .Setup(userManager => userManager.Users).Returns(usersFixture.AsQueryable());
            var list = new List<IdentityRole>()
            {
                new IdentityRole(UserRoleEnum.Admin.ToString()),
                new IdentityRole(UserRoleEnum.User.ToString())
            }
            .AsQueryable();

            _roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                new Mock<IRoleStore<IdentityRole>>().Object,
                new IRoleValidator<IdentityRole>[0],
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<ILogger<RoleManager<IdentityRole>>>().Object);

            _roleManagerMock
                .Setup(r => r.Roles).Returns(list);

        }

        [Fact]
        public async Task GetAll_ReturnsAllUserMovies_UserNotFound()
        {
            // Arrange
            var handler = new GetUserMoviesQueryHandler(_mockUserMovieRepository.Object, _mockUserManager.Object);

            // Act
            await Assert.ThrowsAsync<DomainException>(() => handler.Handle(new GetUserMoviesQuery("user11",
                                                                        It.IsAny<UserMovieFilter?>(),
                                                                        It.IsAny<SortingPaging?>())
                                                                        , CancellationToken.None)
            );

        }
        [Fact]
        public async Task GetAll_ReturnsAllUserMovies()
        {
            // Arrange
            var handler = new GetUserMoviesQueryHandler(_mockUserMovieRepository.Object, _mockUserManager.Object);
            _mockUserMovieRepository.Setup(x => x.GetAll(It.IsAny<string>(), It.IsAny<UserMovieFilter?>(),
                                                     It.IsAny<SortingPaging?>()))
                .ReturnsAsync(new Page<UserMovie>() { Items = _usermovies, TotalItemCount = _usermovies.Count() });
            // Act
            var movieDtos = await handler.Handle(new GetUserMoviesQuery("user1",
                                                                         It.IsAny<UserMovieFilter?>(),
                                                                         It.IsAny<SortingPaging?>())
                                                                         , CancellationToken.None);

            // Assert
            Assert.True(movieDtos.Count() == 5);
            Assert.Equal(_movies.First().Name, movieDtos.First().MovieName);
        }


        [Fact]
        public async Task AddMovie_MovieAlreadyAdded()
        {
            // Arrange
            var movie = _movies.First();
            var movieDtoFixture = _fixture.Build<AddMovieDto>().With(c => c.MovieId, movie.Id).Create();
            UserMovie? userMovie = _usermovies.First();

            _mockUserMovieRepository.Setup(x => x.GetByUserName(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(userMovie);

            // Act
            var handler = new AddUserMovieQueryHandler(_mockUserMovieRepository.Object, _mockMovieRepository.Object, _mockUserManager.Object);
            await Assert.ThrowsAsync<DomainException>(() => 
             handler.Handle(new AddUserMovieQuery("1", movieDtoFixture), CancellationToken.None));

        }
        [Fact]
        public async Task AddMovie_ReturnsMovie()
        {
            // Arrange
            var movie = _movies.First();
            var movieDtoFixture = _fixture.Build<AddMovieDto>().With(c => c.MovieId, movie.Id).Create();
            UserMovie? userMovie = null;

            _mockUserMovieRepository.Setup(x => x.GetByUserName(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(userMovie);
            _mockMovieRepository.Setup(x => x.Add(It.IsAny<Movie>())).ReturnsAsync(movie);
            _mockMovieRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(movie);

            // Act
            var handler = new AddUserMovieQueryHandler(_mockUserMovieRepository.Object, _mockMovieRepository.Object, _mockUserManager.Object);
            var movieDto = await handler.Handle(new AddUserMovieQuery("1", movieDtoFixture), CancellationToken.None);

            // Assert
            Assert.Equal(movieDtoFixture.MovieId, movieDto?.MovieId);
        }

        [Fact]
        public async Task RemoveMovie_ReturnsMovie()
        {
            int movieId = 1;
            // Arrange
            var movieDtoFixture = new RemoveMovieDto { MovieId=movieId};
            UserMovie? userMovie = _usermovies.First();
            _mockUserMovieRepository.Setup(x => x.GetByUserName(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(userMovie);
            _mockUserMovieRepository.Setup(x => x.Delete(It.IsAny<UserMovie>())).ReturnsAsync(true);


            // Act
            var handler = new RemoveUserMovieQueryHandler(_mockUserMovieRepository.Object);
            var movieDto = await handler.Handle(new RemoveUserMovieQuery("user1", movieDtoFixture), CancellationToken.None);

            // Assert
            Assert.Equal(movieDtoFixture.MovieId, movieDto?.MovieId);
        }
    }
}
