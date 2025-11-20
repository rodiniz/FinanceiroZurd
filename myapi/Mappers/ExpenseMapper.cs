using ContextHelpers;
using myapi.Dtos;

namespace myapi.Mappers;

public class ExpenseMapper : IMappperDto<ExpenseDto, Expense>
{
    public ExpenseDto FromEntity(Expense entity)
    {
        return new ExpenseDto
        {
            Id = entity.ExpenseId,
            CategoryId = entity.CategoryId,
            Amount = entity.Amount,
            Date = entity.ExpenseDate,
            Description = entity.Description
        };
    }

    public PagedResult<ExpenseDto> FromEntityList(PagedResult<Expense> entities)
    {
        var result = new PagedResult<ExpenseDto>
        {
            Count = entities.Count,
            Items = [.. entities.Items.Select(FromEntity)]
        };
        return result;
    }

    public Expense ToEntity(ExpenseDto dto)
    {
        return new Expense
        {
            Description = dto.Description,
            ExpenseDate = dto.Date,
            ExpenseId = dto.Id,
            CategoryId = dto.CategoryId,
            Amount = dto.Amount,
            CreatedAt = DateTime.UtcNow
        };
    }
}
