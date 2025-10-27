using System;

namespace myapi.Dtos;

public interface IMappperDto<D,E> where D : class, new() where E : class, new()
{
    List<D> FromEntityList(List<E> entities);
    D FromEntity(E entity);
    E ToEntity(D dto);
}
