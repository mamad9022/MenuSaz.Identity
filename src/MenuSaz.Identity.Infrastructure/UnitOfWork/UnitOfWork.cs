using MenuSaz.Identity.Application.Repositories;
using MenuSaz.Identity.Application.UnitOfWork;
using MenuSaz.Identity.Infrastructure.Repository;
using MenuSaz.Identity.Persistence.Context;

namespace MenuSaz.Identity.Infrastructure.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly UserIdentityContext _dbContext;
    public IUserRepository User { get; }
    public IUserRoleRepository UserRole { get;}

    public UnitOfWork(UserIdentityContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        User = new UserRepository(_dbContext);
        UserRole = new UserRoleRepository(_dbContext);
    }

    public async void Dispose()
    {
        await _dbContext.DisposeAsync();
    }

    public async Task<int> SaveAsync()
    {
        return await _dbContext.SaveChangesAsync();
    }
}
