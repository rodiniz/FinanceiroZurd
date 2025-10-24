using System;
using System.Collections.Generic;

namespace myapi;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<IncomeCategory> IncomeCategories { get; set; } = new List<IncomeCategory>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
}
