namespace MonthlyBudgetCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18April2015d : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BudgetUsers", "Counties", c => c.Int(nullable: false));
            DropColumn("dbo.BudgetUsers", "County");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BudgetUsers", "County", c => c.String(maxLength: 50));
            DropColumn("dbo.BudgetUsers", "Counties");
        }
    }
}
