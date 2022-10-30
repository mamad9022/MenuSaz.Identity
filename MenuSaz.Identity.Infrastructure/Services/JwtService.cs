using MenuSaz.Identity.Application.Configuration;
using MenuSaz.Identity.Application.Services;
using MenuSaz.Identity.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MenuSaz.Identity.Infrastructure.Services
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;
        public JwtService(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
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
                    new Claim("roles", JsonConvert.SerializeObject(user.UserRole.Select(x=>x.Role))),
                    new Claim("phoneNumber", user.PhoneNumber.ToString())
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
    }
}
