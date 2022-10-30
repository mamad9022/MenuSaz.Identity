using MenuSaz.Identity.Application.Repositories;
using MenuSaz.Identity.Domain.Models;
using MenuSaz.Identity.Persistence.Context;

namespace MenuSaz.Identity.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<UserIdentityContext, User>, IUserRepository
    {
        public UserRepository(UserIdentityContext dbContext) : base(dbContext)
        {
        }
    }
}
