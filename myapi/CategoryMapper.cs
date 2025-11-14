using ContextHelpers;
using myapi.Dtos;

namespace myapi;

public class CategoryMapper : IMappperDto<CategoryDto, Category>
{
    public PagedResult<CategoryDto> FromEntityList(PagedResult<Category> entities)
    {
        var result = new PagedResult<CategoryDto>
        {
            Count = entities.Count,
            Items = [.. entities.Items.Select(c => FromEntity(c))]
        };
        return result;
    }

    public CategoryDto FromEntity(Category entity)
    {
        return new CategoryDto
        {
            Id = entity.CategoryId,
            Name = entity.Name,
            UserId = entity.UserId
        };
    }

    public Category ToEntity(CategoryDto dto)
    {
        return new Category
        {
            Name = dto.Name,
            UserId = dto.UserId
        };
    }
}
