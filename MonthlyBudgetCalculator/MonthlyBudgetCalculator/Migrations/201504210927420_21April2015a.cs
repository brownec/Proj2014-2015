namespace MonthlyBudgetCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21April2015a : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Budgets", "TotalExpenses", c => c.Double(nullable: false));
            AddColumn("dbo.Budgets", "BudgetBalance", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Budgets", "BudgetBalance");
            DropColumn("dbo.Budgets", "TotalExpenses");
        }
    }
}
