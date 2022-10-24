using MenuSaz.Identity.Application.Exception;
using MenuSaz.Identity.Application.Services;
using MenuSaz.Identity.Domain.Models;
using MenuSaz.Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace MenuSaz.Identity.Infrastructure.Services;
public class UserService : IUserService
{
    private readonly UserIdentityContext _context;
    public UserService(UserIdentityContext ctx)
    {
        _context = ctx;
    }
    public async Task<User> Create()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetById(long userId)
    {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
            throw new NotFoundException();
        return user;
    }

    public async Task<List<Role>> GetRoles(long userId)
    {
        return await _context.Role.Where(x => x.User.Any(u => u.Id == userId)).ToListAsync();
    }
}
