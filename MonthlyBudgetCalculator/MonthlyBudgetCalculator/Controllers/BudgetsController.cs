using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MonthlyBudgetCalculator.Models;

namespace MonthlyBudgetCalculator.Controllers
{
    public class BudgetsController : Controller
    {
        private MyDBConnection db = new MyDBConnection();

        // Global variables to be accessed throughout controller methods
        double globalTotalIncome = 0;
        double globalTotalCarExpenses = 0;
        double globalTotalHouseholdExpenses = 0;
        double globalTotalPersonalExpenses = 0;
        double globalTotalTravelExpenses = 0;
        double globalTotalUtilityBillExpenses = 0;
        double globalTotalExpenses = 0;
        double globalBudgetBalance = 0;

        // GET: Budgets
        public ActionResult Index()
        {
            var budgets = db.Budgets.Include(b => b.BudgetUser);
            return View(budgets.ToList());
        }

        // GET: Budgets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // GET: Budgets/Create
        public ActionResult Create(int id)
        {
            Budget b = new Budget();
            b.BudgetUserId = id;
            // ViewBag.BudgetUserId = new SelectList(db.BudgetUsers, "BudgetUserId", "LastName");
            return View(b);
        }

        // POST: Budgets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetId,BudgetUserId,BudgetName,BudgetStartDate,BudgetEndDate,IncomePrimaryAmount,IncomeAdditionalAmount,CarTaxAmount,CarInsuranceAmount,CarMaintenanceAmount,CarFuelAmount,CarNctAmount,CarTollChargesAmount,CarExpenseOtherAmount,HouseholdRentMortgageAmount,HouseholdGroceryAmount,HouseholdClothingAmount,HouseholdEducationFeesAmount,HouseholdSchoolSuppliesAmount,HouseholdMedicalExpensesAmount,HouseholdInsuranceAmount,HouseholdMaintenanceAmount,HouseholdExpenseOtherAmount,PersonalSocialAmount,PersonalGymMembershipAmount,PersonalSportsExpenseAmount,PersonalHolidayExpenseAmount,PersonalSavingsAmount,PersonalLoanRepaymentAmount,PersonalHealthInsuranceAmount,PersonalExpenseOtherAmount,TravelBusAmount,TravelLuasAmount,TravelTaxiAmount,TravelTrainAmount,TravelPlaneAmount,TravelExpenseOtherAmount,UtilityBillElectricityAmount,UtilityBillGasAmount,UtilityBillRefuseCollectionAmount,UtilityBillIrishWaterAmount,UtilityBillTVAmount,UtilityBillPhoneBillAmount,UtilityBillBroadbandAmount,UtilityBillOtherExpenseAmount")] Budget budget, int id)
        {
            budget.BudgetUserId = id;
            if (ModelState.IsValid)
            {
                db.Budgets.Add(budget);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BudgetUserId = new SelectList(db.BudgetUsers, "BudgetUserId", "LastName", budget.BudgetUserId);
            return View(budget);
        }

        // GET: Budgets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            ViewBag.BudgetUserId = new SelectList(db.BudgetUsers, "BudgetUserId", "LastName", budget.BudgetUserId);
            return View(budget);
        }

        // POST: Budgets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetId,BudgetUserId,BudgetName,BudgetStartDate,BudgetEndDate,IncomePrimaryAmount,IncomeAdditionalAmount,CarTaxAmount,CarInsuranceAmount,CarMaintenanceAmount,CarFuelAmount,CarNctAmount,CarTollChargesAmount,CarExpenseOtherAmount,HouseholdRentMortgageAmount,HouseholdGroceryAmount,HouseholdClothingAmount,HouseholdEducationFeesAmount,HouseholdSchoolSuppliesAmount,HouseholdMedicalExpensesAmount,HouseholdInsuranceAmount,HouseholdMaintenanceAmount,HouseholdExpenseOtherAmount,PersonalSocialAmount,PersonalGymMembershipAmount,PersonalSportsExpenseAmount,PersonalHolidayExpenseAmount,PersonalSavingsAmount,PersonalLoanRepaymentAmount,PersonalHealthInsuranceAmount,PersonalExpenseOtherAmount,TravelBusAmount,TravelLuasAmount,TravelTaxiAmount,TravelTrainAmount,TravelPlaneAmount,TravelExpenseOtherAmount,UtilityBillElectricityAmount,UtilityBillGasAmount,UtilityBillRefuseCollectionAmount,UtilityBillIrishWaterAmount,UtilityBillTVAmount,UtilityBillPhoneBillAmount,UtilityBillBroadbandAmount,UtilityBillOtherExpenseAmount")] Budget budget, int id)
        {
            budget.BudgetUserId = id;
            if (ModelState.IsValid)
            {
                db.Entry(budget).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BudgetUserId = new SelectList(db.BudgetUsers, "BudgetUserId", "LastName", budget.BudgetUserId);
            return View(budget);
        }

        // GET: Budgets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Budget budget = db.Budgets.Find(id);
            if (budget == null)
            {
                return HttpNotFound();
            }
            return View(budget);
        }

        // POST: Budgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Budget budget = db.Budgets.Find(id);
            db.Budgets.Remove(budget);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

         // Budget Summary View
        public ActionResult Summary(int? id)
        {
            Budget b = new Budget();
            b = db.Budgets.Where(p => p.BudgetId == id).SingleOrDefault();

            // -------------------- INCOME --------------------
            ViewBag.IncomePrimaryAmount = b.IncomePrimaryAmount;
            ViewBag.IncomeAdditionalAmount = b.IncomeAdditionalAmount;
            // Calculate TotalIncome and set result as globalTotalIncome
            b.TotalIncome = (double)b.IncomePrimaryAmount + (double)b.IncomeAdditionalAmount;
            b.TotalIncome = globalTotalIncome;
            // pass TotalIncome to Summary.cshtml
            ViewBag.TotalIncome = globalTotalIncome;

            // -------------------- CAR EXPENDITURE --------------------
            ViewBag.CarTaxAmount = b.CarTaxAmount;
            ViewBag.CarInsuranceAmount = b.CarInsuranceAmount;
            ViewBag.CarMaintenanceAmount = b.CarMaintenanceAmount;
            ViewBag.CarFuelAmount = b.CarFuelAmount;
            ViewBag.CarNctAmount = b.CarNctAmount;
            ViewBag.CarTollChargesAmount = b.CarTollChargesAmount;
            ViewBag.CarExpenseOtherAmount = b.CarExpenseOtherAmount;
            // Calculate TotalCarExpenses and set result as globalTotalCarExpense
            b.TotalCarExpenses = (double)b.CarTaxAmount + (double)b.CarInsuranceAmount + (double)b.CarMaintenanceAmount +
                (double)b.CarFuelAmount + (double)b.CarNctAmount + (double)b.CarTollChargesAmount +
                (double)b.CarExpenseOtherAmount;
            b.TotalCarExpenses = globalTotalCarExpenses;
            // pass TotalCarExpenses to Summary.cshtml
            ViewBag.TotalCarExpenses = globalTotalCarExpenses;

            // -------------------- HOUSEHOLD EXPENDITURE --------------------
            ViewBag.HouseholdRentMortgageAmount = b.HouseholdRentMortgageAmount;
            ViewBag.HouseholdGroceryAmount = b.HouseholdGroceryAmount;
            ViewBag.HouseholdClothingAmount = b.HouseholdClothingAmount;
            ViewBag.HouseholdEducationFeesAmount = b.HouseholdEducationFeesAmount;
            ViewBag.HouseholdSchoolSuppliesAmount = b.HouseholdSchoolSuppliesAmount;
            ViewBag.HouseholdMedicalExpensesAmount = b.HouseholdMedicalExpensesAmount;
            ViewBag.HouseholdInsuranceAmount = b.HouseholdInsuranceAmount;
            ViewBag.HouseholdMaintenanceAmount = b.HouseholdMaintenanceAmount;
            ViewBag.HouseholdExpenseOtherAmount = b.HouseholdExpenseOtherAmount;
            // Calculate TotalHouseholdExpenses and set result as globalTotalHouseholdExpenses
            b.TotalHouseholdExpenses = (double)b.HouseholdRentMortgageAmount + (double)b.HouseholdGroceryAmount +
                (double)b.HouseholdClothingAmount + (double)b.HouseholdEducationFeesAmount +
                (double)b.HouseholdSchoolSuppliesAmount + (double)b.HouseholdMedicalExpensesAmount +
                (double)b.HouseholdInsuranceAmount + (double)b.HouseholdMaintenanceAmount +
                (double)b.HouseholdExpenseOtherAmount;
            b.TotalHouseholdExpenses = globalTotalHouseholdExpenses;
            // pass TotalHouseholdExpenses to Summary.cshtml
            ViewBag.TotalHouseholdExpenses = globalTotalHouseholdExpenses;

            // -------------------- PERSONAL EXPENDITURE --------------------
            ViewBag.PersonalSocialAmount = b.PersonalSocialAmount;
            ViewBag.PersonalGymMembershipAmount = b.PersonalGymMembershipAmount;
            ViewBag.PersonalSportsExpenseAmount = b.PersonalSportsExpenseAmount;
            ViewBag.PersonalHolidayExpenseAmount = b.PersonalHolidayExpenseAmount;
            ViewBag.PersonalSavingsAmount = b.PersonalSavingsAmount;
            ViewBag.PersonalLoanRepaymentAmount = b.PersonalLoanRepaymentAmount;
            ViewBag.PersonalHealthInsuranceAmount = b.PersonalHealthInsuranceAmount;
            ViewBag.PersonalExpenseOtherAmount = b.PersonalExpenseOtherAmount;
            // Calculate TotalPersonalExpenses and set result as globalTotalPersonalExpenses
            b.TotalPersonalExpenses = (double)b.PersonalSocialAmount + (double)b.PersonalGymMembershipAmount +
                (double)b.PersonalSportsExpenseAmount + (double)b.PersonalHolidayExpenseAmount +
                (double)b.PersonalSavingsAmount + (double)b.PersonalLoanRepaymentAmount +
                (double)b.PersonalHealthInsuranceAmount + (double)b.PersonalExpenseOtherAmount;
            b.TotalPersonalExpenses = globalTotalPersonalExpenses;
            // pass TotalPersonalExpenses to Summary.cshtml
            ViewBag.TotalPersonalExpenses = globalTotalPersonalExpenses;

            // -------------------- TRAVEL EXPENDITURE --------------------
            ViewBag.TravelBusAmount = b.TravelBusAmount;
            ViewBag.TravelLuasAmount = b.TravelLuasAmount;
            ViewBag.TravelTaxiAmount = b.TravelTaxiAmount;
            ViewBag.TravelTrainAmount = b.TravelTrainAmount;
            ViewBag.TravelPlaneAmount = b.TravelPlaneAmount;
            ViewBag.TravelExpenseOtherAmount = b.TravelExpenseOtherAmount;
            // Calculate TotalTravelExpenses and set result as globalTotalTravelExpenses
            b.TotalTravelExpenses = (double)b.TravelBusAmount + (double)b.TravelLuasAmount +
                (double)b.TravelTaxiAmount + (double)b.PersonalHolidayExpenseAmount +
                (double)b.TravelTrainAmount + (double)b.TravelPlaneAmount +
                (double)b.TravelExpenseOtherAmount;
            b.TotalTravelExpenses = globalTotalTravelExpenses;
            // pass TotalTravelExpenses to Summary.cshtml
            ViewBag.TotalTravelExpenses = globalTotalTravelExpenses;

            // -------------------- UTILITY BILL EXPENDITURE --------------------
            ViewBag.UtilityBillElectricityAmount = b.UtilityBillElectricityAmount;
            ViewBag.UtilityBillGasAmount = b.UtilityBillGasAmount;
            ViewBag.UtilityBillRefuseCollectionAmount = b.UtilityBillRefuseCollectionAmount;
            ViewBag.UtilityBillIrishWaterAmount = b.UtilityBillIrishWaterAmount;
            ViewBag.UtilityBillTVAmount = b.UtilityBillTVAmount;
            ViewBag.UtilityBillPhoneBillAmount = b.UtilityBillPhoneBillAmount;
            ViewBag.UtilityBillBroadbandAmount = b.UtilityBillBroadbandAmount;
            ViewBag.UtilityBillOtherExpenseAmount = b.UtilityBillOtherExpenseAmount;
            // Calculate TotalUtilityBillExpenses and set result as globalTotalPersonalExpenses
            b.TotalUtilityBillExpenses = (double)b.UtilityBillElectricityAmount + (double)b.UtilityBillGasAmount +
                (double)b.UtilityBillRefuseCollectionAmount + (double)b.UtilityBillIrishWaterAmount +
                (double)b.UtilityBillTVAmount + (double)b.UtilityBillPhoneBillAmount +
                (double)b.UtilityBillBroadbandAmount + (double)b.UtilityBillOtherExpenseAmount;
            b.TotalUtilityBillExpenses = globalTotalUtilityBillExpenses;
            // pass TotalUtilityBillExpenses to Summary.cshtml
            ViewBag.TotalUtilityBillExpenses = globalTotalUtilityBillExpenses;

            // -------------------- SUBTOTAL CALCULATION -------------------- 
            // INCOME - same as TotalIncome calculated above
            // -------------------- TOTAL EXPENSES CALCULATION --------------------
            globalTotalExpenses = (double)globalTotalCarExpenses + (double)globalTotalHouseholdExpenses +
                (double)globalTotalPersonalExpenses + (double)globalTotalTravelExpenses +
                (double)globalTotalUtilityBillExpenses;
            ViewBag.TotalExpenses = globalTotalExpenses;
            // -------------------- BUDGET BALANCE CALCULATION --------------------
            globalBudgetBalance = (double)globalTotalIncome - (double)globalTotalExpenses;
            ViewBag.BudgetBalance = globalBudgetBalance;

            return View(b);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
