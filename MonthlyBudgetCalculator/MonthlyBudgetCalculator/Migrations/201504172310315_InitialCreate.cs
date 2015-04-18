namespace MonthlyBudgetCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Budgets",
                c => new
                    {
                        BudgetId = c.Int(nullable: false, identity: true),
                        BudgetUserId = c.Int(nullable: false),
                        BudgetName = c.String(),
                        BudgetStartDate = c.DateTime(nullable: false),
                        BudgetEndDate = c.DateTime(nullable: false),
                        IncomePrimaryAmount = c.Double(nullable: false),
                        IncomeAdditionalAmount = c.Double(),
                        TotalIncome = c.Double(),
                        CarTaxAmount = c.Double(),
                        CarInsuranceAmount = c.Double(),
                        CarMaintenanceAmount = c.Double(),
                        CarFuelAmount = c.Double(),
                        CarNctAmount = c.Double(),
                        CarTollChargesAmount = c.Double(),
                        CarExpenseOtherAmount = c.Double(),
                        TotalCarExpenses = c.Double(),
                        HouseholdRentMortgageAmount = c.Double(),
                        HouseholdGroceryAmount = c.Double(),
                        HouseholdClothingAmount = c.Double(),
                        HouseholdEducationFeesAmount = c.Double(),
                        HouseholdSchoolSuppliesAmount = c.Double(),
                        HouseholdMedicalExpensesAmount = c.Double(),
                        HouseholdInsuranceAmount = c.Double(),
                        HouseholdMaintenanceAmount = c.Double(),
                        HouseholdExpenseOtherAmount = c.Double(),
                        TotalHouseholdExpenses = c.Double(),
                        PersonalSocialAmount = c.Double(),
                        PersonalGymMembershipAmount = c.Double(),
                        PersonalSportsExpenseAmount = c.Double(),
                        PersonalHolidayExpenseAmount = c.Double(),
                        PersonalSavingsAmount = c.Double(),
                        PersonalLoanRepaymentAmount = c.Double(),
                        PersonalHealthInsuranceAmount = c.Double(),
                        PersonalExpenseOtherAmount = c.Double(),
                        TotalPersonalExpenses = c.Double(),
                        TravelBusAmount = c.Double(),
                        TravelLuasAmount = c.Double(),
                        TravelTaxiAmount = c.Double(),
                        TravelTrainAmount = c.Double(),
                        TravelPlaneAmount = c.Double(),
                        TravelExpenseOtherAmount = c.Double(),
                        TotalTravelExpenses = c.Double(),
                        UtilityBillElectricityAmount = c.Double(),
                        UtilityBillGasAmount = c.Double(),
                        UtilityBillRefuseCollectionAmount = c.Double(),
                        UtilityBillIrishWaterAmount = c.Double(),
                        UtilityBillTVAmount = c.Double(),
                        UtilityBillPhoneBillAmount = c.Double(),
                        UtilityBillBroadbandAmount = c.Double(),
                        UtilityBillOtherExpenseAmount = c.Double(),
                        TotalUtilityBillExpenses = c.Double(),
                    })
                .PrimaryKey(t => t.BudgetId)
                .ForeignKey("dbo.BudgetUsers", t => t.BudgetUserId, cascadeDelete: true)
                .Index(t => t.BudgetUserId);
            
            CreateTable(
                "dbo.BudgetUsers",
                c => new
                    {
                        BudgetUserId = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        AddressLine1 = c.String(nullable: false, maxLength: 50),
                        AddressLine2 = c.String(maxLength: 50),
                        Town = c.String(maxLength: 50),
                        County = c.String(maxLength: 50),
                        Country = c.String(maxLength: 50),
                        PostCode = c.String(),
                        ContactNo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BudgetUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Budgets", "BudgetUserId", "dbo.BudgetUsers");
            DropIndex("dbo.Budgets", new[] { "BudgetUserId" });
            DropTable("dbo.BudgetUsers");
            DropTable("dbo.Budgets");
        }
    }
}
