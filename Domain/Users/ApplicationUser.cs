using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
        public  ICollection<UserMovie> Movies { get; protected set; } = new HashSet<UserMovie>();
    }
}
