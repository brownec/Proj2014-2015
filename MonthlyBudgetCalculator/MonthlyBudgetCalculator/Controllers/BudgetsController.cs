﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MonthlyBudgetCalculator.Models;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using Point = DotNet.Highcharts.Options.Point;
using System.Drawing;

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
            // Calculate TotalIncome
            budget.TotalIncome = (double)budget.IncomePrimaryAmount + (double)budget.IncomeAdditionalAmount;
            
            // Calculate TotalCarExpenses
            budget.TotalCarExpenses = (double)budget.CarTaxAmount + (double)budget.CarInsuranceAmount + (double)budget.CarMaintenanceAmount +
                (double)budget.CarFuelAmount + (double)budget.CarNctAmount + (double)budget.CarTollChargesAmount +
                (double)budget.CarExpenseOtherAmount;

            // Calculate TotalHouseholdExpenses
            budget.TotalHouseholdExpenses = (double)budget.HouseholdRentMortgageAmount + (double)budget.HouseholdGroceryAmount +
                (double)budget.HouseholdClothingAmount + (double)budget.HouseholdEducationFeesAmount +
                (double)budget.HouseholdSchoolSuppliesAmount + (double)budget.HouseholdMedicalExpensesAmount +
                (double)budget.HouseholdInsuranceAmount + (double)budget.HouseholdMaintenanceAmount +
                (double)budget.HouseholdExpenseOtherAmount;
            
            // Calculate TotalPersonalExpenses
            budget.TotalPersonalExpenses = (double)budget.PersonalSocialAmount + (double)budget.PersonalGymMembershipAmount +
                (double)budget.PersonalSportsExpenseAmount + (double)budget.PersonalHolidayExpenseAmount +
                (double)budget.PersonalSavingsAmount + (double)budget.PersonalLoanRepaymentAmount +
                (double)budget.PersonalHealthInsuranceAmount + (double)budget.PersonalExpenseOtherAmount;

            // Calculate TotalTravelExpenses
            budget.TotalTravelExpenses = (double)budget.TravelBusAmount + (double)budget.TravelLuasAmount +
                (double)budget.TravelTaxiAmount + (double)budget.PersonalHolidayExpenseAmount +
                (double)budget.TravelTrainAmount + (double)budget.TravelPlaneAmount +
                (double)budget.TravelExpenseOtherAmount;

            // Calculate TotalUtilityBillExpenses
            budget.TotalUtilityBillExpenses = (double)budget.UtilityBillElectricityAmount + (double)budget.UtilityBillGasAmount +
                (double)budget.UtilityBillRefuseCollectionAmount + (double)budget.UtilityBillIrishWaterAmount +
                (double)budget.UtilityBillTVAmount + (double)budget.UtilityBillPhoneBillAmount +
                (double)budget.UtilityBillBroadbandAmount + (double)budget.UtilityBillOtherExpenseAmount;

            // Calculate Subtotals
            budget.TotalExpenses = (double)budget.TotalCarExpenses + (double)budget.TotalHouseholdExpenses +
                (double)budget.TotalPersonalExpenses + (double)budget.TotalTravelExpenses +
                (double)budget.TotalUtilityBillExpenses;

            // Calculate Budget Balance
            budget.BudgetBalance = (double)budget.TotalIncome - (double)budget.TotalExpenses;

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

        // ******************** BUDGET ANALYSIS SUMMARY ********************
        public ActionResult Summary(int? id)
        {
            Budget b = new Budget();
            b = db.Budgets.Where(p => p.BudgetId == id).SingleOrDefault();

            //// -------------------- INCOME --------------------
            //double totalIncome = 0;
            //ViewBag.IncomePrimaryAmount = b.IncomePrimaryAmount;
            //ViewBag.IncomeAdditionalAmount = b.IncomeAdditionalAmount;
            //// Calculate TotalIncome and set result as globalTotalIncome
            //// sets totalIncome initially equal to primaryIncome
            //totalIncome = (double)b.IncomePrimaryAmount;
            //// if there is Additional Income execute the following
            //if(b.IncomeAdditionalAmount != null)
            //{
            //    totalIncome = (double)b.IncomePrimaryAmount + (double)b.IncomeAdditionalAmount;
            //}
            //// pass TotalIncome to Summary.cshtml
            //ViewBag.TotalIncome = totalIncome;
            //// totalIncome = globalTotalIncome;
            //// -------------------- END OF INCOME --------------------

            //// -------------------- CAR EXPENDITURE --------------------
            //double totalCarExpenses = 0;
            //ViewBag.CarTaxAmount = b.CarTaxAmount;
            //ViewBag.CarInsuranceAmount = b.CarInsuranceAmount;
            //ViewBag.CarMaintenanceAmount = b.CarMaintenanceAmount;
            //ViewBag.CarFuelAmount = b.CarFuelAmount;
            //ViewBag.CarNctAmount = b.CarNctAmount;
            //ViewBag.CarTollChargesAmount = b.CarTollChargesAmount;
            //ViewBag.CarExpenseOtherAmount = b.CarExpenseOtherAmount;
            //// Calculate TotalCarExpenses and set result as globalTotalCarExpense
            //totalCarExpenses = (double)b.CarTaxAmount + (double)b.CarInsuranceAmount + (double)b.CarMaintenanceAmount +
            //    (double)b.CarFuelAmount + (double)b.CarNctAmount + (double)b.CarTollChargesAmount +
            //    (double)b.CarExpenseOtherAmount;
            //// pass TotalCarExpenses to Summary.cshtml
            //ViewBag.TotalCarExpenses = totalCarExpenses;
            //// totalCarExpenses = b.TotalCarExpenses;
            //// -------------------- END OF CAR EXPENDITURE --------------------

            //// -------------------- HOUSEHOLD EXPENDITURE --------------------
            //double totalHouseholdExpenses = 0;
            //ViewBag.HouseholdRentMortgageAmount = b.HouseholdRentMortgageAmount;
            //ViewBag.HouseholdGroceryAmount = b.HouseholdGroceryAmount;
            //ViewBag.HouseholdClothingAmount = b.HouseholdClothingAmount;
            //ViewBag.HouseholdEducationFeesAmount = b.HouseholdEducationFeesAmount;
            //ViewBag.HouseholdSchoolSuppliesAmount = b.HouseholdSchoolSuppliesAmount;
            //ViewBag.HouseholdMedicalExpensesAmount = b.HouseholdMedicalExpensesAmount;
            //ViewBag.HouseholdInsuranceAmount = b.HouseholdInsuranceAmount;
            //ViewBag.HouseholdMaintenanceAmount = b.HouseholdMaintenanceAmount;
            //ViewBag.HouseholdExpenseOtherAmount = b.HouseholdExpenseOtherAmount;
            //// Calculate TotalHouseholdExpenses
            //totalHouseholdExpenses = (double)b.HouseholdRentMortgageAmount + (double)b.HouseholdGroceryAmount +
            //    (double)b.HouseholdClothingAmount + (double)b.HouseholdEducationFeesAmount +
            //    (double)b.HouseholdSchoolSuppliesAmount + (double)b.HouseholdMedicalExpensesAmount +
            //    (double)b.HouseholdInsuranceAmount + (double)b.HouseholdMaintenanceAmount +
            //    (double)b.HouseholdExpenseOtherAmount;
            //// pass TotalHouseholdExpenses to Summary.cshtml
            //ViewBag.TotalHouseholdExpenses = totalHouseholdExpenses;
            //// totalHouseholdExpenses = globalTotalHouseholdExpenses;
            //// -------------------- END OF HOUSEHOLD EXPENDITURE --------------------

            //// -------------------- PERSONAL EXPENDITURE --------------------
            //double totalPersonalExpenses = 0;
            //ViewBag.PersonalSocialAmount = b.PersonalSocialAmount;
            //ViewBag.PersonalGymMembershipAmount = b.PersonalGymMembershipAmount;
            //ViewBag.PersonalSportsExpenseAmount = b.PersonalSportsExpenseAmount;
            //ViewBag.PersonalHolidayExpenseAmount = b.PersonalHolidayExpenseAmount;
            //ViewBag.PersonalSavingsAmount = b.PersonalSavingsAmount;
            //ViewBag.PersonalLoanRepaymentAmount = b.PersonalLoanRepaymentAmount;
            //ViewBag.PersonalHealthInsuranceAmount = b.PersonalHealthInsuranceAmount;
            //ViewBag.PersonalExpenseOtherAmount = b.PersonalExpenseOtherAmount;
            //// Calculate TotalPersonalExpenses and set result as globalTotalPersonalExpenses
            //totalPersonalExpenses = (double)b.PersonalSocialAmount + (double)b.PersonalGymMembershipAmount +
            //    (double)b.PersonalSportsExpenseAmount + (double)b.PersonalHolidayExpenseAmount +
            //    (double)b.PersonalSavingsAmount + (double)b.PersonalLoanRepaymentAmount +
            //    (double)b.PersonalHealthInsuranceAmount + (double)b.PersonalExpenseOtherAmount;
            //// pass TotalPersonalExpenses to Summary.cshtml
            //ViewBag.TotalPersonalExpenses = totalPersonalExpenses;
            //// totalPersonalExpenses = globalTotalPersonalExpenses;
            //// -------------------- END OF PERSONAL EXPENDITURE --------------------

            //// -------------------- TRAVEL EXPENDITURE --------------------
            //double totalTravelExpenses = 0;
            //ViewBag.TravelBusAmount = b.TravelBusAmount;
            //ViewBag.TravelLuasAmount = b.TravelLuasAmount;
            //ViewBag.TravelTaxiAmount = b.TravelTaxiAmount;
            //ViewBag.TravelTrainAmount = b.TravelTrainAmount;
            //ViewBag.TravelPlaneAmount = b.TravelPlaneAmount;
            //ViewBag.TravelExpenseOtherAmount = b.TravelExpenseOtherAmount;
            //// Calculate TotalTravelExpenses and set result as globalTotalTravelExpenses
            //totalTravelExpenses = (double)b.TravelBusAmount + (double)b.TravelLuasAmount +
            //    (double)b.TravelTaxiAmount + (double)b.PersonalHolidayExpenseAmount +
            //    (double)b.TravelTrainAmount + (double)b.TravelPlaneAmount +
            //    (double)b.TravelExpenseOtherAmount;
            //// pass TotalTravelExpenses to Summary.cshtml
            //ViewBag.TotalTravelExpenses = totalTravelExpenses;
            //// totalTravelExpenses = globalTotalTravelExpenses;
            //// -------------------- END OF TRAVEL EXPENDITURE --------------------

            //// -------------------- UTILITY BILL EXPENDITURE --------------------
            //double totalUtilityBillExpenses = 0;
            //ViewBag.UtilityBillElectricityAmount = b.UtilityBillElectricityAmount;
            //ViewBag.UtilityBillGasAmount = b.UtilityBillGasAmount;
            //ViewBag.UtilityBillRefuseCollectionAmount = b.UtilityBillRefuseCollectionAmount;
            //ViewBag.UtilityBillIrishWaterAmount = b.UtilityBillIrishWaterAmount;
            //ViewBag.UtilityBillTVAmount = b.UtilityBillTVAmount;
            //ViewBag.UtilityBillPhoneBillAmount = b.UtilityBillPhoneBillAmount;
            //ViewBag.UtilityBillBroadbandAmount = b.UtilityBillBroadbandAmount;
            //ViewBag.UtilityBillOtherExpenseAmount = b.UtilityBillOtherExpenseAmount;
            //// Calculate TotalUtilityBillExpenses and set result as globalTotalPersonalExpenses
            //totalUtilityBillExpenses = (double)b.UtilityBillElectricityAmount + (double)b.UtilityBillGasAmount +
            //    (double)b.UtilityBillRefuseCollectionAmount + (double)b.UtilityBillIrishWaterAmount +
            //    (double)b.UtilityBillTVAmount + (double)b.UtilityBillPhoneBillAmount +
            //    (double)b.UtilityBillBroadbandAmount + (double)b.UtilityBillOtherExpenseAmount;
            //// pass TotalUtilityBillExpenses to Summary.cshtml
            //ViewBag.TotalUtilityBillExpenses = totalUtilityBillExpenses;
            //// totalUtilityBillExpenses = globalTotalUtilityBillExpenses;
            //// -------------------- END OF UTILITY BILL EXPENDITURE --------------------

            //// -------------------- SUBTOTAL CALCULATION -------------------- 
            //// INCOME - same as TotalIncome calculated above
            //// -------------------- TOTAL EXPENSES CALCULATION --------------------
            //double totalExpenses = 0;
            //totalExpenses = (double)totalCarExpenses + (double)totalHouseholdExpenses +
            //    (double)totalPersonalExpenses + (double)totalTravelExpenses +
            //    (double)totalUtilityBillExpenses;
            //ViewBag.TotalExpenses = totalExpenses;
            //// totalExpenses = globalTotalExpenses;
            //// -------------------- BUDGET BALANCE CALCULATION --------------------
            //double budgetBalance = 0;
            //budgetBalance = (double)totalIncome - (double)totalExpenses;
            //ViewBag.BudgetBalance = budgetBalance;
            //// budgetBalance = globalBudgetBalance;
            return View(b);
        }

        // ******************** BUDGET ANALYSIS FORECAST ********************
        public ActionResult Forecast(int? id)
        {
            Budget b = new Budget();
            // return list of budgets specific to one user
            b = db.Budgets.Where(user => user.BudgetUserId == id).SingleOrDefault();

            // -------------------- BUBBLE SORT --------------------
            // -------------------- BUDGET BALANCES --------------------
            /* idea to sort the list of budget balances and display top 5
             * based on whether they highest balances or lowest balances
            */
            //var balances = from e in db.Budgets where e.BudgetUserId == id select e;
            //int size = balances.Count(); // get the size of the number of results
            //object[] budgetBalances = new object[size];
            //for (int i = budgetBalances.Length - 1; i > 0; i--)
            //{
            //    for (int j = 0; j < i - 1; j++)
            //    {
            //        if (budgetBalances[j] <= budgetBalances[j + 1])
            //        {
            //            int maximumBalance = budgetBalances[j];
            //            budgetBalances[j] = budgetBalances[j + 1];
            //            budgetBalances[j + 1] = maximumBalance;
            //        }
            //    }
            //}

            //int i; 
            //int j = 0;
            //int minimumBudgetBalance, tempBudgetBalance;
            //for(i = 0; i <= j; i ++)
            //{
            //    minimumBudgetBalance = i;
            //    for(int inner = i + 1; inner <= j; inner ++ )
            //    if(b[inner] < b[minimumBudgetBalance])
            //        minimumBudgetBalance = inner;
            //    tempBudgetBalance = b[j];
            //    b[j] = b[minimumBudgetBalance];
            //    b[minimumBudgetBalance] = tempBudgetBalance;
            //    return minimumBudgetBalance;
            //}

            // -------------------- SEARCHING ALGORITHM --------------------
            // -------------------- MINIMUM BUDGET BALANCE --------------------
            /* idea to return budget balances with lowest balance
               and display
             * i)assign 1st element in array as minimum value
             * ii)loop through array and compare each element in the array
             * iii)if accessed variable value less than current minimum value, replace
            */
            //static int FindMinimumBudgetBalance(int[] b) // b is array(maybe change)
            //{
            //    int minimumBudgetBalance = b[0];
            //    for(int i = 0; i < b.Length - 1; i++)
            //        if(b[index] < minimumBudgetBalance)
            //            minimumBudgetBalance = b[index];
            //    return minimumBudgetBalance;
            //}

            // -------------------- MAXIMUM BUDGET BALANCE --------------------
            /* idea to return budget balances with highest balance
               and display
             * i)assign 1st array element to variable that stores maximum value
             * ii)loop through array comparing each element with value stored in variable
             * iii)replace current value if accessed value is greater
            */
            //static int FindMaximumBudgetBalance(int[] b)
            //{
            //    int maximumBudgetBalance = b[0];
            //    for(int i = 0; i < b.Length - 1; i++)
            //        if(b[index] > maximumBudgetBalance)
            //            maximumBudgetBalance = b[index];
            //    return maximumBudgetBalance;
            //}
            
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
