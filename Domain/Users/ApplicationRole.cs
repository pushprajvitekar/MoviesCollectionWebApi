using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Users
{
    public partial class UserLogin : IdentityUserLogin<string>
    {
    }
    public partial class UserRole : IdentityUserRole<string>
    {
    }
    public partial class UserClaim : IdentityUserClaim<string>
    {
    }
    public partial class Role : IdentityRole<string>
    {
        public Role() : base()
        {
        }

        public Role(string roleName)
        {
            Name = roleName;
        }
    }
    public partial class RoleClaim : IdentityRoleClaim<string>
    {
    }
    public partial class UserToken : IdentityUserToken<string>
    {
    }
}
