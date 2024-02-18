using Application.Users.Commands.AddMovie;
using Application.Users.Commands.RemoveMovie;
using Application.Users.Dtos;
using Application.Users.Queries.GetUserMovies;
using Application.Common;
using Domain.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollectionWebApi.Auth;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesCollectionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = $"{Roles.Admin},{Roles.User}")]
    public class UsersController : ControllerBase
    {

        private readonly IMediator mediator;
        private readonly IAuthorizationService authorizationService;

        public UsersController(IMediator mediator, IAuthorizationService authorizationService)
        {
            this.mediator = mediator;
            this.authorizationService = authorizationService;
        }


       



        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserMovies([FromQuery] UserMovieFilter? filter, [FromQuery] SortingPaging? sortingPaging)
        {
            var res = await mediator.Send(new GetUserMoviesQuery(filter, sortingPaging));
            return Ok(res);
        }

        // POST api/<UsersController>
        [HttpPost("{id}")]
        public async Task<IActionResult> AddMovie(int id, [FromBody] AddMovieDto addMovieDto)
        {
            if (addMovieDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User, id, UserAuthorizationRequirement.SameUser);

            if (authorizationResult.Succeeded)
            {
                var res = await mediator.Send(new AddUserMovieQuery(addMovieDto));
                return CreatedAtAction(nameof(AddMovie), new { res });
            }
            else
            {
                return User.Identity?.IsAuthenticated == true ? new ForbidResult() : new ChallengeResult();
            }
        }

        //// PUT api/<UsersController>/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateMovie([FromBody] string value)
        //{

        //    if (updateMovieDto == null)
        //    {
        //        return BadRequest();
        //    }
        //    var res = await mediator.Send(new UpdateMovieRequest(updateMovieDto));
        //    return AcceptedAtAction(nameof(UpdateMovie), new { res });
        //}

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveMovie(int id, [FromBody] RemoveMovieDto removeMovieDto)
        {
            if (removeMovieDto == null)
            {
                return BadRequest();
            }
            var authorizationResult = await authorizationService.AuthorizeAsync(User, id, UserAuthorizationRequirement.SameUser);

            if (authorizationResult.Succeeded)
            {
                var res = await mediator.Send(new RemoveUserMovieQuery(removeMovieDto));
                return Ok(new { res });
            }
            else
            {
                return User.Identity?.IsAuthenticated == true ? new ForbidResult() : new ChallengeResult();
            }
        }
    }
}
