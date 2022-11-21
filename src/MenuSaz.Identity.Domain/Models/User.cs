using MediatR;
using Nitro.Fund.Backend.Domain.Common;
using System.Text.Json.Serialization;

namespace MenuSaz.Identity.Domain.Models
{
    public class User : Entity<long>
    {
        public User()
        {

        }

        public User(string username, string firstname, string lastname, string password, long phoneNumber, bool isActive)
        {
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Password = password;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
        }

        public void AddRole(List<Role> roles, User user)
        {
            if (roles is null) return;
            foreach (var role in roles)
            {
                _userRoles = new List<UserRole> { new Models.UserRole(user.Id, user, role.Id, role) };
            }
        }

        private List<UserRole> _userRoles;

        public string Username { get; private set; } = null!;
        public string Firstname { get; private set; } = null!;
        public string Lastname { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public long PhoneNumber { get; private set; }
        public bool IsActive { get; private set; }
        public List<UserRole> UserRole => _userRoles;
    }
}
