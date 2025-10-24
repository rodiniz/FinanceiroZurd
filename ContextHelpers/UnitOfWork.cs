
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OneOf;
using OneOf.Types;

namespace ContextHelpers;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(DbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public IRepository<T> Repository<T>() where T : class, new()
    {
        if (!repositories.ContainsKey(typeof(T)))
        {
            var repository = _serviceProvider.GetService<IRepository<T>>();

            repositories.Add(typeof(T), repository);

            return repository;
        }

        return repositories[typeof(T)] as IRepository<T>;
    }

    public async Task<OneOf<Success, Exception>> CompleteAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            return new Success();
        }
        catch (Exception ex)
        {
            return new Exception(ex.Message);
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
