using Nitro.Fund.Backend.Domain.Common;

namespace MenuSaz.Identity.Domain.Models
{
    public class UserRole : Entity<long>
    {
        public UserRole()
        {

        }
        public UserRole(long userId, User user, long roleId, Role role)
        {
            UserId = userId;
            User = user;
            RoleId = roleId;
            Role = role;
        }

        public long UserId { get; private set; }
        public User User { get; private set; }
        public long RoleId { get; private set; }
        public Role Role { get; private set; }

    }
}
