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

        public User(string username, string firstname, string lastname, string password, bool isActive)
        {
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Password = password;
            IsActive = isActive;
        }

        public void AddRole(List<Role> roles)
        {
            if (roles is null) return;
                _roles = roles;
        }

        private List<Role> _roles;

        public string Username { get; private set; } = null!;
        public string Firstname { get; private set; } = null!;
        public string Lastname { get; private set; } = null!;
        public string Password { get; private set; } = null!;
        public long PhoneNumber { get; private set; }
        public bool IsActive { get; private set; }
        public List<Role> Role => _roles;
    }
}
