using ContextHelpers;
using Microsoft.EntityFrameworkCore;
using myapi.Infrastructure;
using System.Linq.Expressions;
public class Repository<T>(NeondbContext _context) : IRepository<T> where T : class, new()
{
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<PagedResult<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int skip, int take, string orderBy)
    {
        var query = _context.Set<T>().AsNoTracking()
             .Where(predicate)
             .OrderByDynamic(orderBy);

        var count = query.Count();

        var items = await query.Skip(skip)
            .Take(take)
            .ToListAsync();

        return new PagedResult<T> { Count = count, Items = items };
    }

    public async Task UpdateAsync(int id, T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(id);
        if (existingEntity == null)
        {
            throw new ArgumentException($"Entity with id {id} not found.");
        }
        _context.UpdateEntityValues(existingEntity, entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        await _context.Set<T>()
                .Where(predicate)
                .ExecuteDeleteAsync();
    }
}
