using Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MovieCollectionWebApi.Auth.Jwt
{
    public class TokenService
    {
        private const int ExpirationMinutes = 60;
        private readonly ILogger<TokenService> _logger;
        private readonly string? validIssuer;
        private string? validAudience;
        private byte[] symmetricSecurityKey;

        public TokenService(ILogger<TokenService> logger, IConfiguration configuration)
        {
            _logger = logger;
            validIssuer = configuration["Data:Jwt:Issuer"];
            validAudience = configuration["Data:Jwt:Audience"];
            symmetricSecurityKey = Encoding.UTF8.GetBytes(configuration["Data:Jwt:Key"] ?? throw new ArgumentNullException(nameof(configuration), $"Jwt Key not defined"));
        }

        public string CreateToken(IdentityUser user, IEnumerable<string> roles)
        {
            var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
            var token = CreateJwtToken(
                CreateClaims(user, roles),
                CreateSigningCredentials(),
                expiration
            );
            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.LogInformation("JWT Token created");

            return tokenHandler.WriteToken(token);
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
            DateTime expiration) =>
            new(
                validIssuer,
                validAudience,
                claims,
                expires: expiration,
                signingCredentials: credentials
            );

        private List<Claim> CreateClaims(IdentityUser user, IEnumerable<string> roles)
        {
            // var jwtSub = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("JwtTokenSettings")["JwtRegisteredClaimNamesSub"];

            try
            {
                var claims = new List<Claim>
            {
               //new Claim(JwtRegisteredClaimNames.Sub, jwtSub),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName??"Unknown"),
                new Claim(ClaimTypes.Email, user.Email ?? "Unknown" ),
            };
                foreach (var userRole in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                return claims;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(new SymmetricSecurityKey(symmetricSecurityKey), SecurityAlgorithms.HmacSha256);
        }
    }
}
