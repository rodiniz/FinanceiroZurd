using System;
using System.Collections.Generic;

namespace myapi;

public partial class Income
{
    public int IncomeId { get; set; }

    public int? UserId { get; set; }

    public int? IncomeCategoryId { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public DateOnly IncomeDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual IncomeCategory? IncomeCategory { get; set; }

    public virtual User? User { get; set; }
}
