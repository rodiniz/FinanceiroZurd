using ContextHelpers;

namespace myapi.Dtos;

public interface IMappperDto<D, E> where D : class, new() where E : class, new()
{
    PagedResult<D> FromEntityList(PagedResult<E> entities);
    D FromEntity(E entity);
    E ToEntity(D dto);
    List<D> FromEntityList(List<E> entities);
}
