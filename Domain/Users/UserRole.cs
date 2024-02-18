namespace Domain.Users
{
    public class UserRole
    {
        public UserRole(UserRoleEnum role)
        {
            Id = (int)role;
            Name = role.ToString();
        }

        public int Id { get; protected set; }
        public string Name { get; protected set; }
    }
}
