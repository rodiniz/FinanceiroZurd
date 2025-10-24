using System;
using System.Collections.Generic;

namespace myapi;

public partial class Category
{
    public int CategoryId { get; set; }

    public int? UserId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual User? User { get; set; }
}
