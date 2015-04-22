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
    public class BudgetUsersController : Controller
    {
        private MyDBConnection db = new MyDBConnection();

        // GET: BudgetUsers
        public ActionResult Index()
        {
            return View(db.BudgetUsers.ToList());
        }

        // GET: BudgetUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetUser budgetUser = db.BudgetUsers.Find(id);
            if (budgetUser == null)
            {
                return HttpNotFound();
            }
            return View(budgetUser);
        }

        // GET: BudgetUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BudgetUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BudgetUserId,LastName,FirstName,DateOfBirth,AddressLine1,AddressLine2,Town,Counties,Country,PostCode,ContactNo")] BudgetUser budgetUser)
        {
            if (ModelState.IsValid)
            {
                db.BudgetUsers.Add(budgetUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(budgetUser);
        }

        // GET: BudgetUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetUser budgetUser = db.BudgetUsers.Find(id);
            if (budgetUser == null)
            {
                return HttpNotFound();
            }
            return View(budgetUser);
        }

        // POST: BudgetUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BudgetUserId,LastName,FirstName,DateOfBirth,AddressLine1,AddressLine2,Town,Counties,Country,PostCode,ContactNo")] BudgetUser budgetUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(budgetUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(budgetUser);
        }

        // GET: BudgetUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BudgetUser budgetUser = db.BudgetUsers.Find(id);
            if (budgetUser == null)
            {
                return HttpNotFound();
            }
            return View(budgetUser);
        }

        // POST: BudgetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BudgetUser budgetUser = db.BudgetUsers.Find(id);
            db.BudgetUsers.Remove(budgetUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // ******************** BUDGET ANALYSIS CHARTS ********************
        public ActionResult Charts(int? id)
        {
            BudgetUser u = new BudgetUser();
            // return list of budgets specific to one user
            u = db.BudgetUsers.Where(user => user.BudgetUserId == id).SingleOrDefault();

            var total = from e in db.Budgets where e.BudgetUserId == id select e;

            int size = total.Count();
            // System.Diagnostics.Debug.WriteLine("size: " + size);

            // DotNet.Highcharts.Highcharts chart = new DotNet.Highcharts.Highcharts("chart")
            object[] income = new object[size];
            int c1 = 0;

            foreach (var item in total)
            {
                income[c1] = item.TotalIncome;
                c1++;
            }

            String[] budgetNames = new string[size];
            int c2 = 0;
            foreach (var item in total)
            {
                budgetNames[c2] = item.BudgetName;
                c2++;
            }
            int value = 0;
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
                Text = "Monthly Budget Analysis Charts "
            })

            .SetSubtitle(new Subtitle
            {
                Text = " Total Income Analysis Chart "
            })

            .SetXAxis(new XAxis
            {
                Categories = budgetNames
                //Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
            })

            .SetYAxis(new YAxis
            {
                Title = new YAxisTitle
                {
                    Text = "Total Income in €"
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

            //.SetSeries(new Series
             .SetSeries(new []
            {
                new Series{Name = "Total Income",Data = new Data(income)}
                //Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
            })

            .SetCredits(new Credits
            {
                Enabled = false
            }); // remove hyperlink for highchart
        
            return View(chart);
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
