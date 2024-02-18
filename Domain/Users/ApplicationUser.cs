using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public class ApplicationUser : IdentityUser
    {
       // public UserRoleEnum Role { get; set; }
        public virtual ICollection<UserMovie> Movies { get; protected set; } = new HashSet<UserMovie>();
    }
}
