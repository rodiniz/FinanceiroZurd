using OneOf;
using OneOf.Types;

namespace ContextHelpers;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : class, new();
    Task<OneOf<Success, Exception>> CompleteAsync();
}