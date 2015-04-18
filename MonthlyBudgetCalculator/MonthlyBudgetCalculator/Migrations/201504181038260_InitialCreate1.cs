namespace MonthlyBudgetCalculator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Budgets", "IncomeAdditionalAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TotalIncome", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "CarTaxAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "CarInsuranceAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "CarMaintenanceAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "CarFuelAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "CarNctAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "CarTollChargesAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "CarExpenseOtherAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TotalCarExpenses", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "HouseholdRentMortgageAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "HouseholdGroceryAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "HouseholdClothingAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "HouseholdEducationFeesAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "HouseholdSchoolSuppliesAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "HouseholdMedicalExpensesAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "HouseholdInsuranceAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "HouseholdMaintenanceAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "HouseholdExpenseOtherAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TotalHouseholdExpenses", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "PersonalSocialAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "PersonalGymMembershipAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "PersonalSportsExpenseAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "PersonalHolidayExpenseAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "PersonalSavingsAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "PersonalLoanRepaymentAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "PersonalHealthInsuranceAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "PersonalExpenseOtherAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TotalPersonalExpenses", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TravelBusAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TravelLuasAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TravelTaxiAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TravelTrainAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TravelPlaneAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TravelExpenseOtherAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TotalTravelExpenses", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "UtilityBillElectricityAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "UtilityBillGasAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "UtilityBillRefuseCollectionAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "UtilityBillIrishWaterAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "UtilityBillTVAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "UtilityBillPhoneBillAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "UtilityBillBroadbandAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "UtilityBillOtherExpenseAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.Budgets", "TotalUtilityBillExpenses", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Budgets", "TotalUtilityBillExpenses", c => c.Double());
            AlterColumn("dbo.Budgets", "UtilityBillOtherExpenseAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "UtilityBillBroadbandAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "UtilityBillPhoneBillAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "UtilityBillTVAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "UtilityBillIrishWaterAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "UtilityBillRefuseCollectionAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "UtilityBillGasAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "UtilityBillElectricityAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TotalTravelExpenses", c => c.Double());
            AlterColumn("dbo.Budgets", "TravelExpenseOtherAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TravelPlaneAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TravelTrainAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TravelTaxiAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TravelLuasAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TravelBusAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TotalPersonalExpenses", c => c.Double());
            AlterColumn("dbo.Budgets", "PersonalExpenseOtherAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "PersonalHealthInsuranceAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "PersonalLoanRepaymentAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "PersonalSavingsAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "PersonalHolidayExpenseAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "PersonalSportsExpenseAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "PersonalGymMembershipAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "PersonalSocialAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TotalHouseholdExpenses", c => c.Double());
            AlterColumn("dbo.Budgets", "HouseholdExpenseOtherAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "HouseholdMaintenanceAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "HouseholdInsuranceAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "HouseholdMedicalExpensesAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "HouseholdSchoolSuppliesAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "HouseholdEducationFeesAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "HouseholdClothingAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "HouseholdGroceryAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "HouseholdRentMortgageAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TotalCarExpenses", c => c.Double());
            AlterColumn("dbo.Budgets", "CarExpenseOtherAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "CarTollChargesAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "CarNctAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "CarFuelAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "CarMaintenanceAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "CarInsuranceAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "CarTaxAmount", c => c.Double());
            AlterColumn("dbo.Budgets", "TotalIncome", c => c.Double());
            AlterColumn("dbo.Budgets", "IncomeAdditionalAmount", c => c.Double());
        }
    }
}
