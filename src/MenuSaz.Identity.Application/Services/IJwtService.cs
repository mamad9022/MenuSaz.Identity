using MenuSaz.Identity.Domain.Models;

namespace MenuSaz.Identity.Application.Services;
public interface IJwtService
{
    public Task<string> GenerateJwtToken(User user);
    public Task<bool> ValidateJwtToken(string token);
}
