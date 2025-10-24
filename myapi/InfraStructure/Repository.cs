using System.Linq.Expressions;
using ContextHelpers;
using Microsoft.EntityFrameworkCore;
using myapi.Infrastructure;
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

    public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int? skip = null, int? take = null)
    {
        var query = _context.Set<T>().Where(predicate);
        if (skip.HasValue)
        {
            query = query.Skip(skip.Value);
        }
        if (take.HasValue)
        {
            query = query.Take(take.Value);
        }
        return await query.ToListAsync();
    }

    public async Task UpdateAsync(int id, T entity)
    {
        var existingEntity = await _context.Set<T>().FindAsync(id);
        if (existingEntity == null)
        {
            throw new ArgumentException($"Entity with id {id} not found.");
        }
        _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expression<Func<T, bool>> predicate)
    {
        await _context.Set<T>()
                .Where(predicate)
                .ExecuteDeleteAsync();
    }
}
