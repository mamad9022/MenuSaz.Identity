using MenuSaz.Identity.Application.Repositories;
using MenuSaz.Identity.Domain.Models;
using MenuSaz.Identity.Persistence.Context;

namespace MenuSaz.Identity.Infrastructure.Repository
{
    public class UserRoleRepository : BaseRepository<UserIdentityContext, UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(UserIdentityContext dbContext) : base(dbContext)
        {
        }
    }
}
