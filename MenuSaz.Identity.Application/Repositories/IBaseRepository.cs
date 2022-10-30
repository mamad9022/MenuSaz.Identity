using Nitro.Fund.Backend.Domain.Common;
using System.Linq.Expressions;

namespace MenuSaz.Identity.Application.Repositories
{
    public interface IBaseRepository<T> where T : Entity<long>
    {
        Task<IReadOnlyList<T>> GetAllAsync();

        Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object?>>>? includes = null,
            int? skipCount = null,
            int? takeCount = null,
            bool disableTracking = true);

        Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object?>>>? includes = null,
            bool disableTracking = true);

        Task<T?> GetByIdAsync(long id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        Task CommitAsync();
    }
}
