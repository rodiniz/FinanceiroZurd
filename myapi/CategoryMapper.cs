using System;
using myapi.Dtos;

namespace myapi;

public class CategoryMapper : IMappperDto<CategoryDto, Category>
{
    public List<CategoryDto> FromEntityList(List<Category> entities)
    {
        return entities.Select(e => new CategoryDto
        {
            Id = e.CategoryId,
            Name = e.Name
        }).ToList();
    }

    public CategoryDto FromEntity(Category entity)
    {
        return new CategoryDto
        {
            Id = entity.CategoryId,
            Name = entity.Name,
            UserId=entity.UserId
        };
    }

    public  Category ToEntity(CategoryDto dto)
    {
        return new Category
        {           
            Name = dto.Name,
            UserId = dto.UserId
        };
    }
}
