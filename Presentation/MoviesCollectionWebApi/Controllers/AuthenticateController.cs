using Application.Users.Dtos;
using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieCollectionWebApi.Auth;
using MovieCollectionWebApi.Auth.Jwt;

namespace MoviesCollectionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TokenService tokenService;

        public AuthenticateController(
            UserManager<ApplicationUser> userManager,
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
            var user = await _userManager.FindByNameAsync(model.UserName);
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

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            if (!await _roleManager.RoleExistsAsync(UserRoleEnum.User.ToString()))
                await _roleManager.CreateAsync(new IdentityRole(UserRoleEnum.User.ToString()) { NormalizedName = UserRoleEnum.User.ToString().ToUpper() });
            await _userManager.AddToRoleAsync(user, UserRoleEnum.User.ToString());
            return CreatedAtAction(nameof(Register), new { email = model.Email, role = UserRoleEnum.User.ToString() }, new RegisterUserDto(model.Email, model.Username, string.Empty));
        }

        [HttpPost]
        [Route("register-admin")]
       // [Authorize(Roles = Roles.Admin)]//todo add configuration to enable /disable this api
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterUserDto model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new GenericResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            await AddRoles();

            if (await _roleManager.RoleExistsAsync(UserRoleEnum.Admin.ToString()))
            {
                await _userManager.AddToRoleAsync(user, UserRoleEnum.Admin.ToString());
            }
            return Ok(new GenericResponse { Status = "Success", Message = "User created successfully!" });
        }

        private async Task AddRoles()
        {
            var roles = Enum.GetNames(typeof(UserRoleEnum));
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                    await _roleManager.CreateAsync(new IdentityRole(role) { NormalizedName = role.ToUpper() });
            }
        }
    }
}
