using System;
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

            // -------------------- INCOME --------------------
            double totalIncome = 0;
            ViewBag.IncomePrimaryAmount = b.IncomePrimaryAmount;
            ViewBag.IncomeAdditionalAmount = b.IncomeAdditionalAmount;
            // Calculate TotalIncome and set result as globalTotalIncome
            // sets totalIncome initially equal to primaryIncome
            totalIncome = (double)b.IncomePrimaryAmount;
            // if there is Additional Income execute the following
            if(b.IncomeAdditionalAmount != null)
            {
                totalIncome = (double)b.IncomePrimaryAmount + (double)b.IncomeAdditionalAmount;
            }
            // pass TotalIncome to Summary.cshtml
            ViewBag.TotalIncome = totalIncome;
            // totalIncome = globalTotalIncome;
            // -------------------- END OF INCOME --------------------

            // -------------------- CAR EXPENDITURE --------------------
            double totalCarExpenses = 0;
            ViewBag.CarTaxAmount = b.CarTaxAmount;
            ViewBag.CarInsuranceAmount = b.CarInsuranceAmount;
            ViewBag.CarMaintenanceAmount = b.CarMaintenanceAmount;
            ViewBag.CarFuelAmount = b.CarFuelAmount;
            ViewBag.CarNctAmount = b.CarNctAmount;
            ViewBag.CarTollChargesAmount = b.CarTollChargesAmount;
            ViewBag.CarExpenseOtherAmount = b.CarExpenseOtherAmount;
            // Calculate TotalCarExpenses and set result as globalTotalCarExpense
            totalCarExpenses = (double)b.CarTaxAmount + (double)b.CarInsuranceAmount + (double)b.CarMaintenanceAmount +
                (double)b.CarFuelAmount + (double)b.CarNctAmount + (double)b.CarTollChargesAmount +
                (double)b.CarExpenseOtherAmount;
            // pass TotalCarExpenses to Summary.cshtml
            ViewBag.TotalCarExpenses = totalCarExpenses;
            // totalCarExpenses = b.TotalCarExpenses;
            // -------------------- END OF CAR EXPENDITURE --------------------

            // -------------------- HOUSEHOLD EXPENDITURE --------------------
            double totalHouseholdExpenses = 0;
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
            totalHouseholdExpenses = (double)b.HouseholdRentMortgageAmount + (double)b.HouseholdGroceryAmount +
                (double)b.HouseholdClothingAmount + (double)b.HouseholdEducationFeesAmount +
                (double)b.HouseholdSchoolSuppliesAmount + (double)b.HouseholdMedicalExpensesAmount +
                (double)b.HouseholdInsuranceAmount + (double)b.HouseholdMaintenanceAmount +
                (double)b.HouseholdExpenseOtherAmount;
            // pass TotalHouseholdExpenses to Summary.cshtml
            ViewBag.TotalHouseholdExpenses = totalHouseholdExpenses;
            // totalHouseholdExpenses = globalTotalHouseholdExpenses;
            // -------------------- END OF HOUSEHOLD EXPENDITURE --------------------

            // -------------------- PERSONAL EXPENDITURE --------------------
            double totalPersonalExpenses = 0;
            ViewBag.PersonalSocialAmount = b.PersonalSocialAmount;
            ViewBag.PersonalGymMembershipAmount = b.PersonalGymMembershipAmount;
            ViewBag.PersonalSportsExpenseAmount = b.PersonalSportsExpenseAmount;
            ViewBag.PersonalHolidayExpenseAmount = b.PersonalHolidayExpenseAmount;
            ViewBag.PersonalSavingsAmount = b.PersonalSavingsAmount;
            ViewBag.PersonalLoanRepaymentAmount = b.PersonalLoanRepaymentAmount;
            ViewBag.PersonalHealthInsuranceAmount = b.PersonalHealthInsuranceAmount;
            ViewBag.PersonalExpenseOtherAmount = b.PersonalExpenseOtherAmount;
            // Calculate TotalPersonalExpenses and set result as globalTotalPersonalExpenses
            totalPersonalExpenses = (double)b.PersonalSocialAmount + (double)b.PersonalGymMembershipAmount +
                (double)b.PersonalSportsExpenseAmount + (double)b.PersonalHolidayExpenseAmount +
                (double)b.PersonalSavingsAmount + (double)b.PersonalLoanRepaymentAmount +
                (double)b.PersonalHealthInsuranceAmount + (double)b.PersonalExpenseOtherAmount;
            // pass TotalPersonalExpenses to Summary.cshtml
            ViewBag.TotalPersonalExpenses = totalPersonalExpenses;
            // totalPersonalExpenses = globalTotalPersonalExpenses;
            // -------------------- END OF PERSONAL EXPENDITURE --------------------

            // -------------------- TRAVEL EXPENDITURE --------------------
            double totalTravelExpenses = 0;
            ViewBag.TravelBusAmount = b.TravelBusAmount;
            ViewBag.TravelLuasAmount = b.TravelLuasAmount;
            ViewBag.TravelTaxiAmount = b.TravelTaxiAmount;
            ViewBag.TravelTrainAmount = b.TravelTrainAmount;
            ViewBag.TravelPlaneAmount = b.TravelPlaneAmount;
            ViewBag.TravelExpenseOtherAmount = b.TravelExpenseOtherAmount;
            // Calculate TotalTravelExpenses and set result as globalTotalTravelExpenses
            totalTravelExpenses = (double)b.TravelBusAmount + (double)b.TravelLuasAmount +
                (double)b.TravelTaxiAmount + (double)b.PersonalHolidayExpenseAmount +
                (double)b.TravelTrainAmount + (double)b.TravelPlaneAmount +
                (double)b.TravelExpenseOtherAmount;
            // pass TotalTravelExpenses to Summary.cshtml
            ViewBag.TotalTravelExpenses = totalTravelExpenses;
            // totalTravelExpenses = globalTotalTravelExpenses;
            // -------------------- END OF TRAVEL EXPENDITURE --------------------

            // -------------------- UTILITY BILL EXPENDITURE --------------------
            double totalUtilityBillExpenses = 0;
            ViewBag.UtilityBillElectricityAmount = b.UtilityBillElectricityAmount;
            ViewBag.UtilityBillGasAmount = b.UtilityBillGasAmount;
            ViewBag.UtilityBillRefuseCollectionAmount = b.UtilityBillRefuseCollectionAmount;
            ViewBag.UtilityBillIrishWaterAmount = b.UtilityBillIrishWaterAmount;
            ViewBag.UtilityBillTVAmount = b.UtilityBillTVAmount;
            ViewBag.UtilityBillPhoneBillAmount = b.UtilityBillPhoneBillAmount;
            ViewBag.UtilityBillBroadbandAmount = b.UtilityBillBroadbandAmount;
            ViewBag.UtilityBillOtherExpenseAmount = b.UtilityBillOtherExpenseAmount;
            // Calculate TotalUtilityBillExpenses and set result as globalTotalPersonalExpenses
            totalUtilityBillExpenses = (double)b.UtilityBillElectricityAmount + (double)b.UtilityBillGasAmount +
                (double)b.UtilityBillRefuseCollectionAmount + (double)b.UtilityBillIrishWaterAmount +
                (double)b.UtilityBillTVAmount + (double)b.UtilityBillPhoneBillAmount +
                (double)b.UtilityBillBroadbandAmount + (double)b.UtilityBillOtherExpenseAmount;
            // pass TotalUtilityBillExpenses to Summary.cshtml
            ViewBag.TotalUtilityBillExpenses = totalUtilityBillExpenses;
            // totalUtilityBillExpenses = globalTotalUtilityBillExpenses;
            // -------------------- END OF UTILITY BILL EXPENDITURE --------------------

            // -------------------- SUBTOTAL CALCULATION -------------------- 
            // INCOME - same as TotalIncome calculated above
            // -------------------- TOTAL EXPENSES CALCULATION --------------------
            double totalExpenses = 0;
            totalExpenses = (double)totalCarExpenses + (double)totalHouseholdExpenses +
                (double)totalPersonalExpenses + (double)totalTravelExpenses +
                (double)totalUtilityBillExpenses;
            ViewBag.TotalExpenses = totalExpenses;
            // totalExpenses = globalTotalExpenses;
            // -------------------- BUDGET BALANCE CALCULATION --------------------
            double budgetBalance = 0;
            budgetBalance = (double)totalIncome - (double)totalExpenses;
            ViewBag.BudgetBalance = budgetBalance;
            // budgetBalance = globalBudgetBalance;
            return View(b);
        }

        // ******************** BUDGET ANALYSIS CHARTS ********************
        public ActionResult Charts(int? id)
        {
            Budget b = new Budget();
            // return list of budgets specific to one user
            b = db.Budgets.Where(user => user.BudgetUserId == id).SingleOrDefault();

            // DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            Highcharts chart = new Highcharts("chart")

            .InitChart(new Chart
            {
                DefaultSeriesType = ChartTypes.Line,
                MarginRight = 130,
                MarginBottom = 25,
                ClassName = "chart"
            })



            .SetTitle(new Title
            {
                Text = "Monthly Budget: " + b.BudgetName
            })

            .SetSubtitle(new Subtitle
            {
                Text = " Budget Analysis Chart "
            })

            .SetXAxis(new XAxis
            {
                Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
            })

            .SetYAxis(new YAxis
            {
                Title = new YAxisTitle
                {
                    Text = "TEXT HERE"
                },
                PlotLines = new[]
                { 
                    new YAxisPlotLines
                    {
                        Value = 0,
                        Width = 1,
                        Color = ColorTranslator.FromHtml("#808080")
                    }
                }
            })

            .SetTooltip(new Tooltip
            {
                Crosshairs = new Crosshairs(true, true)
            })

            .SetLegend(new Legend
            {
                Layout = Layouts.Vertical,
                Align = HorizontalAligns.Right,
                VerticalAlign = VerticalAligns.Top,
                X = -10,
                Y = 100,
                BorderWidth = 0
            })

            .SetSeries(new Series
            {
                Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
            })

            .SetCredits(new Credits
            {
                Enabled = false
            }); // remove hyperlink for highchart

            return View(chart);
        }

        // ******************** BUDGET ANALYSIS FORECAST ********************
        public ActionResult Forecast(int? id)
        {
            Budget b = new Budget();
            // return list of budgets specific to one user
            b = db.Budgets.Where(user => user.BudgetUserId == id).SingleOrDefault();

            // -------------------- SELECTION SORT --------------------
            // -------------------- BUDGET BALANCES --------------------
            /* idea to sort the list of budget balances and display top 5
             * based on whether they highest balances or lowest balances
            */
            
            //int upper, inner;
            //int minimumBudgetBalance, tempBudgetBalance;
            //for(int outer = 0; outer <= upper; outer ++)
            //{
            //    minimumBudgetBalance = outer;
            //    for(int inner = outer + 1; inner <= upper; inner ++ )
            //    if(b[inner] < b[minimumBudgetBalance])
            //        minimumBudgetBalance = inner;
            //    tempBudgetBalance = b[outer];
            //    b[outer] = b[minimumBudgetBalance];
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
