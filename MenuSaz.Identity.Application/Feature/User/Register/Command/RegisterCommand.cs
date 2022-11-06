using MenuSaz.Identity.Application.Common;
using MenuSaz.Identity.Application.Feature.User.Dtos;

namespace MenuSaz.Identity.Application.Feature.User.Command
{
    public class RegisterCommand : CommandBase<UserDto>
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public long Phonenumber { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
