
using System.Linq.Expressions;

namespace ContextHelpers;

public interface IRepository<T> where T : class, new()
{
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate,  int? skip = null, int? take = null);
    Task AddAsync(T entity);
    Task UpdateAsync(int id, T entity);
    Task DeleteAsync(Expression<Func<T, bool>> predicate);
}
