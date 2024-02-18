using Domain.Auth;
using Microsoft.AspNetCore.Identity;

namespace Domain.Users
{
    public class User : ApplicationUser
    {
        //public User(string name, string loginName, string hashedPassword, UserRoleEnum userRole)
        //{
        //    Name = name ?? throw new ArgumentNullException(nameof(name));
        //    UserName = loginName ?? throw new ArgumentNullException(nameof(loginName));
        //    HashedPassword = hashedPassword ?? throw new ArgumentNullException(nameof(hashedPassword));
        //    Role =  new UserRole(userRole);
        //}

        //public string Name { get; protected set; }

        //public string LoginName { get; protected set; }

        //public string HashedPassword { get; protected set; }
        //public UserRole Role { get; protected set; }
        public virtual ICollection<UserMovie> Movies { get; protected set; } = new HashSet<UserMovie>();

        public UserMovie AddMovie(UserMovie movie)
        {
            Movies.Add(movie);
            return movie;
        }

        public void RemoveMovie(UserMovie movie)
        {
            Movies.Remove(movie);
        }


    }
}
