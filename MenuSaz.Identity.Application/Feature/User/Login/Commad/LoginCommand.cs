using MenuSaz.Identity.Application.Common;
using MenuSaz.Identity.Application.Feature.User.Login.Dtos;

namespace MenuSaz.Identity.Application.Feature.User.Login.Commad;
public class LoginCommand : CommandBase<LoginDto>
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}
