using MenuSaz.Identity.Application.Repositories;
using MenuSaz.Identity.Domain.Models;
using MenuSaz.Identity.Persistence.Context;
using System.Linq.Expressions;

namespace MenuSaz.Identity.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<UserIdentityContext, User>, IUserRepository
    {
        public UserRepository(UserIdentityContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> DuplicateUsername(string username)
        {
            var t = await FirstOrDefaultAsync(x => x.Username == username);
        return t != null;
        }
    }
}
