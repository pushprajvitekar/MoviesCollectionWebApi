using EFCorePersistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace MoviesCollectionWebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void RegisterDBContext(this IServiceCollection services, IConfiguration configuration)
        { 
        var connString= configuration["Data:DefaultConnection:ConnectionString"] ?? throw new ArgumentNullException(nameof(configuration), $"ConnectionString not defined");
            services.AddDbContext<MovieCollectionDBContext>(options => options.UseSqlServer(connString));
        }

        public static void AddAuthenticationJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            // These will eventually be moved to a secrets file, but for alpha development appsettings is fine
            var validIssuer = configuration["Data:Jwt:Issuer"];
            var validAudience = configuration["Data:Jwt:Audience"];
            var symmetricSecurityKey = Encoding.UTF8.GetBytes(configuration["Data:Jwt:Key"] ?? throw new ArgumentNullException(nameof(configuration), $"Jwt Key not defined"));
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = validIssuer,
                        ValidAudience = validAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(symmetricSecurityKey)
                        
                    };
                });

        }
    }
}
