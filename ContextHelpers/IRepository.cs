
using System.Linq.Expressions;

namespace ContextHelpers;

public interface IRepository<T> where T : class, new()
{
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    Task<PagedResult<T>> FindAllAsync(Expression<Func<T, bool>> predicate, int skip, int take, string orderBy);
    Task AddAsync(T entity);
    Task UpdateAsync(int id, T entity);
    Task DeleteAsync(Expression<Func<T, bool>> predicate);
}
