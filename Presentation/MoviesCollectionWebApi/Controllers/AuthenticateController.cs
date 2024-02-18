using Application.Users.Dtos;
using Azure;
using Azure.Core;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MovieCollectionWebApi.Auth;
using MovieCollectionWebApi.Auth.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MoviesCollectionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService tokenService;

        public AuthenticateController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            TokenService tokenService
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            this.tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user?.Email != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var token = tokenService.CreateToken(user, userRoles);

                return Ok(new AuthResponse
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = token,
                });
            }
            return Unauthorized();
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            await _userManager.AddToRoleAsync(user, UserRoleEnum.User.ToString());
            return CreatedAtAction(nameof(Register), new { email = model.Email, role = UserRoleEnum.User.ToString() }, new RegisterUserDto(model.Email,model.Username,string.Empty));
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoleEnum.Admin.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRoleEnum.Admin.ToString()));
            if (!await _roleManager.RoleExistsAsync(UserRoleEnum.User.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRoleEnum.User.ToString()));

            if (await _roleManager.RoleExistsAsync(UserRoleEnum.Admin.ToString()))
            {
                await _userManager.AddToRoleAsync(user, UserRoleEnum.Admin.ToString());
            }
            //if (await _roleManager.RoleExistsAsync(UserRoleEnum.User.ToString()))
            //{
            //    await _userManager.AddToRoleAsync(user, UserRoleEnum.User.ToString());
            //}
            return Ok(new GenericResponse { Status = "Success", Message = "User created successfully!" });
        }

    }
}
