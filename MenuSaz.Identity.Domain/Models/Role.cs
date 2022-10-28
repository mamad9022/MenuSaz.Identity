using Nitro.Fund.Backend.Domain.Common;
using System.Text.Json.Serialization;

namespace MenuSaz.Identity.Domain.Models;
    public class Role : Entity<long>
    {
        public Role(string title, bool isActive)
        {
            Title = title;
            IsActive = isActive;
        }


        public string Title { get; private set; }
        public bool IsActive { get; private set; }
        public List<UserRole> UserRole { get; private set; }
}
