using Application.Movies.Commands.CreateMovie;
using Application.Movies.Commands.UpdateMovie;
using Application.Movies.Dtos;
using Application.Movies.Queries.GetMovies;
using Application.Common;
using Domain.Movies.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollectionWebApi.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesCollectionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MoviesController : ControllerBase
    {
        private readonly IMediator mediator;

        public MoviesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        // GET: api/<MoviesController>
        [HttpGet]
        [Authorize(Roles = $"{Roles.Manager},{Roles.Admin},{Roles.User}")]
        public async Task<IActionResult> GetMovies([FromQuery] MovieFilter? filter, [FromQuery] SortingPaging? sortingPaging)
        {
            var res = await mediator.Send(new GetMoviesQuery(filter, sortingPaging));
            return Ok(res);
        }


        // POST api/<MoviesController>
        [HttpPost]
         [Authorize(Roles = $"{Roles.Manager},{Roles.Admin}")]
        public async Task<IActionResult> CreateMovie([FromBody] CreateMovieDto createMovieDto)
        {
            if (createMovieDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var res = await mediator.Send(new CreateMovieRequest(createMovieDto));
            return CreatedAtAction(nameof(CreateMovie), res);
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = $"{Roles.Manager},{Roles.Admin}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] UpdateMovieDto updateMovieDto)
        {
            if (updateMovieDto == null)
            {
                return BadRequest();
            }
            var res = await mediator.Send(new UpdateMovieRequest(id, updateMovieDto));
            return AcceptedAtAction(nameof(UpdateMovie), res);
        }

        //// DELETE api/<MoviesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
