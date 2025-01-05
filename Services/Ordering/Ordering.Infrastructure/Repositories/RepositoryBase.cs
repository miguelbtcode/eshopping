namespace Ordering.Infrastructure.Repositories;

using System.Linq.Expressions;
using Core.Common;
using Core.Repositories;
using Data;
using Microsoft.EntityFrameworkCore;

public class RepositoryBase<T>(OrderContext dbContext) : IAsyncRepository<T>
    where T : EntityBase
{
    protected readonly OrderContext dbContext = dbContext;

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await dbContext.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await dbContext.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }
    
    public async Task<T> AddAsync(T entity)
    {
        dbContext.Set<T>().Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }      
}