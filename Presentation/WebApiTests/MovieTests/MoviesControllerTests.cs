using Application.Common;
using Application.Movies.Dtos;
using Application.Movies.Queries.GetMovies;
using Domain.Movies;
using Domain.Movies.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MoviesCollectionWebApi.Controllers;

namespace WebApiTests.MovieTests
{
    public class MoviesControllerTests
    {


        [Fact]
        public async Task GetAllMovies_ReturnsOkResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var expectedResult = new List<MovieDto> { 
                new(1, "Movie 1", "", MovieGenreEnum.War.ToString())
            };

            mediatorMock.Setup(m => m.Send(It.IsAny<GetMoviesQuery>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(expectedResult);

            var controller = new MoviesController(mediatorMock.Object);

            // Act
            var result = await controller.GetMovies(
                It.IsAny<MovieFilter?>(),It.IsAny<SortingPaging?>());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualmovies = Assert.IsAssignableFrom<IEnumerable<MovieDto>>(okResult.Value);
            Assert.Single(actualmovies);
            Assert.Equal(expectedResult.First().Name, actualmovies.First().Name);
            Assert.Equal(expectedResult.First().Genre, actualmovies.First().Genre);
        }

        [Fact]
        public async Task GetAllMoviesByName_Returns_NotFoundResult()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();


            mediatorMock.Setup(m => m.Send(It.IsAny<GetMoviesQuery>(), It.IsAny<CancellationToken>()))
                                            .ReturnsAsync(new List<MovieDto>());

            var controller = new MoviesController(mediatorMock.Object);

            // Act
            var result = await controller.GetMovies(
                It.IsAny<MovieFilter?>(), It.IsAny<SortingPaging?>());
            // Act

            // Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
        }
    }
}
