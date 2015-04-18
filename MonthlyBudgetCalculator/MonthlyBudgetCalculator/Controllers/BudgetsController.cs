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
        public ActionResult Edit([Bind(Include = "BudgetId,BudgetUserId,BudgetName,BudgetStartDate,BudgetEndDate,IncomePrimaryAmount,IncomeAdditionalAmount,CarTaxAmount,CarInsuranceAmount,CarMaintenanceAmount,CarFuelAmount,CarNctAmount,CarTollChargesAmount,CarExpenseOtherAmount,HouseholdRentMortgageAmount,HouseholdGroceryAmount,HouseholdClothingAmount,HouseholdEducationFeesAmount,HouseholdSchoolSuppliesAmount,HouseholdMedicalExpensesAmount,HouseholdInsuranceAmount,HouseholdMaintenanceAmount,HouseholdExpenseOtherAmount,PersonalSocialAmount,PersonalGymMembershipAmount,PersonalSportsExpenseAmount,PersonalHolidayExpenseAmount,PersonalSavingsAmount,PersonalLoanRepaymentAmount,PersonalHealthInsuranceAmount,PersonalExpenseOtherAmount,TravelBusAmount,TravelLuasAmount,TravelTaxiAmount,TravelTrainAmount,TravelPlaneAmount,TravelExpenseOtherAmount,UtilityBillElectricityAmount,UtilityBillGasAmount,UtilityBillRefuseCollectionAmount,UtilityBillIrishWaterAmount,UtilityBillTVAmount,UtilityBillPhoneBillAmount,UtilityBillBroadbandAmount,UtilityBillOtherExpenseAmount")] Budget budget)
        {
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
