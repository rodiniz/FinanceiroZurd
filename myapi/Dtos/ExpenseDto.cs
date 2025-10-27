using System;
using System.Security.Claims;

namespace myapi.Dtos;

public class ExpenseDto(){
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateOnly Date { get; set; }
    public string? Description { get; set; } = null!;
    public int? CategoryId { get; set; } 
    
}
