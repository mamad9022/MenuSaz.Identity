using MenuSaz.Identity.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Nitro.Fund.Backend.Domain.Common;
using System.Linq.Expressions;

namespace MenuSaz.Identity.Infrastructure.Repository
{
    public class BaseRepository<TDbContext, T> : IBaseRepository<T>
        where T : Entity<long> where TDbContext : DbContext
    {
        private readonly TDbContext _dbContext;

        public BaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object?>>>? includes = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        protected async Task<long> GetTotalAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).CountAsync();
        }

        protected async Task<IReadOnlyList<T>> GetWithPagingAsync(Expression<Func<T, bool>> predicate,
          int pageNumber,
          int pageSize,
          Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            return await GetAsync(predicate, orderBy, null, pageNumber * pageSize, pageSize);
        }


        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            List<Expression<Func<T, object?>>>? includes = null,
            int? skipCount = null,
            int? takeCount = null,
            bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking) query = query.AsNoTracking();
            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
            if (predicate != null) query = query.Where(predicate);
            if (orderBy != null) query = orderBy(query);
            if (skipCount != null) query = query.Skip(skipCount.Value);
            if (takeCount != null) query = query.Take(takeCount.Value);
            return await query.ToListAsync();

        }

        public virtual async Task<T?> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
