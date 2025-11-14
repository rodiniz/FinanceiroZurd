using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContextHelpers;

public static class DynamicHelpers
{
    public static IQueryable<T> OrderByDynamic<T>(
       this IQueryable<T> source,
       string propertyName,
       bool descending = false)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, propertyName);
        var lambda = Expression.Lambda(property, parameter);
        string methodName = descending ? "OrderByDescending" : "OrderBy";
        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.Type },
            source.Expression,
            Expression.Quote(lambda));
        return source.Provider.CreateQuery<T>(resultExpression);
    }
    public static void UpdateEntityValues<TEntity>(
            this DbContext context,
            TEntity existingEntity,
            TEntity newEntity) where TEntity : class
    {
        var entry = context.Entry(existingEntity);

        // Get the key names for this entity
        var keyNames = entry.Metadata.FindPrimaryKey().Properties
                                     .Select(p => p.Name)
                                     .ToHashSet();

        foreach (var property in entry.OriginalValues.Properties)
        {
            if (keyNames.Contains(property.Name))
                continue; // skip primary key(s)

            var newValue = context.Entry(newEntity).CurrentValues[property];
            if (newValue is not null)
            {
                entry.Property(property.Name).CurrentValue = newValue;
            }
        }
    }

}
