using MenuSaz.Identity.Domain.Models;
using System.Linq.Expressions;

namespace MenuSaz.Identity.Application.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> DuplicateUsername(string username);
    }
}
