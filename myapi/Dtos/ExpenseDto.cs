namespace myapi.Dtos;

public record ExpenseDto(int Id, decimal Amount, DateOnly Date, string? Description, int? CategoryId);
