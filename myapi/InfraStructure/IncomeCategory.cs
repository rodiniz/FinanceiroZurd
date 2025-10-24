using System;
using System.Collections.Generic;

namespace myapi;

public partial class IncomeCategory
{
    public int IncomeCategoryId { get; set; }

    public string? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ApplicationUser? User { get; set; }
}
