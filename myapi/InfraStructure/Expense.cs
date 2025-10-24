using System;
using System.Collections.Generic;

namespace myapi;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public string UserId { get; set; }

    public int? CategoryId { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateOnly ExpenseDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ApplicationUser? User { get; set; }
}
