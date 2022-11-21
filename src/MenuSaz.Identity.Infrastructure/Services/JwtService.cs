using MenuSaz.Identity.Application.Configuration;
using MenuSaz.Identity.Application.Services;
using MenuSaz.Identity.Application.UnitOfWork;
using MenuSaz.Identity.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace MenuSaz.Identity.Infrastructure.Services;
public class JwtService : IJwtService
{
    private readonly AppSettings _appSettings;
    private readonly IUnitOfWork _unitOfWork;
    public JwtService(IOptions<AppSettings> options, IUnitOfWork unitOfWork)
    {
        _appSettings = options.Value;
        _unitOfWork = unitOfWork;
    }
    public async Task<string> GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                    new Claim("userId", user.Id.ToString()),
                    new Claim("roles", JsonConvert.SerializeObject(await GetUserRolesAsync(user.Id))),
                    new Claim("phonenumber", user.PhoneNumber.ToString()),
                    new Claim("username", user.Username),
                    new Claim("firstname", user.Firstname),
                    new Claim("lastname", user.Lastname)
                }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = Task.FromResult(tokenHandler.CreateToken(tokenDescriptor));
        return tokenHandler.WriteToken(await token);
    }
    public async Task<bool> ValidateJwtToken(string token)
    {
        if (token == null)
            return false;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        try
        {
            await Task.FromResult(tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken));

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims.Any(x => x.Type == "userId");
        }
        catch
        {
            return false;
        }
    }

    #region GetUserRolesAsync
    private async Task<IReadOnlyList<string>> GetUserRolesAsync(long userId)
    {
        var userRoles = await _unitOfWork.UserRole.GetAsync(x => x.UserId == userId &&
        !x.IsDeleted, null, new List<Expression<Func<UserRole, object?>>>
        {
                x => x.Role
        });
        return userRoles.Select(x => x.Role.Title).ToList();
    }
    #endregion
}
