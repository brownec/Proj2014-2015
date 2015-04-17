using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BudgetCalculator.Models;

namespace MonthlyBudgetCalculator.Models
{
    public class MyDBConnection : DbContext
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetUser> BudgetUsers { get; set; }
    }
}