using MenuSaz.Identity.Domain.Models;

namespace MenuSaz.Identity.Application.Services
{
    public interface IUserService
    {
        public Task<User?> GetById(long userId);
        public Task<User> Create();
        public Task<List<Role>> GetRoles(long userId);
    }
}
