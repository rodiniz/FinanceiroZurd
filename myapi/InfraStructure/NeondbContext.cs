using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace myapi.Infrastructure;

public partial class NeondbContext : IdentityDbContext<ApplicationUser>
{
    public NeondbContext()
    {
    }

    public NeondbContext(DbContextOptions<NeondbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }
    public virtual DbSet<IncomeCategory> IncomeCategories { get; set; }  

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //set a diferent name to the identity table
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable(name: "categories");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.HasKey(e => e.ExpenseId);
            entity.ToTable(name: "expenses");
            entity.Property(e => e.ExpenseId).HasColumnName("expense_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });
        
        modelBuilder.Entity<Income>(entity =>
        {          
            entity.HasKey(e => e.IncomeId);
            entity.ToTable(name: "income");
            entity.Property(e => e.IncomeId).HasColumnName("income_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });
     
     
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
