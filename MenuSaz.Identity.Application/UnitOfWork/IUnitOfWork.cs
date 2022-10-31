using MenuSaz.Identity.Application.Repositories;

namespace MenuSaz.Identity.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IUserRoleRepository UserRole { get; }
        Task<int> SaveAsync();
    }
}
