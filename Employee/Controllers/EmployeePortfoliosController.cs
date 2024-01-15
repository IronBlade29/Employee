using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Employee.Models;

namespace Employee.Controllers
{
    public class EmployeePortfoliosController : Controller
    {
        private EmployeePortfolioContext db = new EmployeePortfolioContext();

        // GET: EmployeePortfolios
        //public ActionResult Index()
        //{
        //    return View(db.EmployeePortfolios.ToList());
        //}

        public ActionResult Index(string email)
        {
            if (email != "" && email!=null)
            {
                EmployeePortfolio employeePortfolio = db.EmployeePortfolios.FirstOrDefault(x => x.Email == email);
                if (employeePortfolio != null)
                    return View(employeePortfolio);
                else
                    return View("Create");
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        // GET: EmployeePortfolios/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EmployeePortfolio employeePortfolio = db.EmployeePortfolios.Find(id);
        //    if (employeePortfolio == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employeePortfolio);
        //}

        // GET: EmployeePortfolios/Create
        public ActionResult Create()
        {
            var allSkills = db.Skills.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToList();
            ViewBag.Skills = new SelectList(allSkills, "Value", "Text");
            return View();
        }

        // POST: EmployeePortfolios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Projects,Education,Achievements,References,Email")] EmployeePortfolio employeePortfolio)
        {
            if (ModelState.IsValid)
            {
                db.EmployeePortfolios.Add(employeePortfolio);
                db.SaveChanges();
                return RedirectToAction("Index",new {email=employeePortfolio.Email });
            }
            var Skills = db.Skills.ToList();
            ViewBag.Skills = new MultiSelectList(Skills, "Id", "Name");

            return View(employeePortfolio);
        }

        // GET: EmployeePortfolios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePortfolio employeePortfolio = db.EmployeePortfolios.Find(id);
            if (employeePortfolio == null)
            {
                return HttpNotFound();
            }
            return View(employeePortfolio);
        }

        // POST: EmployeePortfolios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonalInformation,Education,Achievements,References")] EmployeePortfolio employeePortfolio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeePortfolio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeePortfolio);
        }

        // GET: EmployeePortfolios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeePortfolio employeePortfolio = db.EmployeePortfolios.Find(id);
            if (employeePortfolio == null)
            {
                return HttpNotFound();
            }
            return View(employeePortfolio);
        }

        // POST: EmployeePortfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeePortfolio employeePortfolio = db.EmployeePortfolios.Find(id);
            db.EmployeePortfolios.Remove(employeePortfolio);
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
