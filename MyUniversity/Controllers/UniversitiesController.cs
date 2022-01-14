using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyUniversity.Models;

namespace MyUniversity.Controllers
{
    public class UniversitiesController : Controller
    {
        private UniversityDBContext db = new UniversityDBContext();

        // GET: Universities
        public ActionResult Index(string universityDepartment, string searchString)
        {
            var Department = new List<string>();

            var DepartmentQry = from d in db.Universities
                           orderby d.Department
                           select d.Department;

            Department.AddRange(DepartmentQry.Distinct());
            ViewBag.universityDepartment = new SelectList(Department);

            var universities = from m in db.Universities
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                universities = universities.Where(s => s.Institute.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(universityDepartment))
            {
                universities = universities.Where(x => x.Department == universityDepartment);
            }

            return View(universities);
        }

        // GET: Universities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // GET: Universities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Universities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Institute,dateofreporting,Course,Department,year")] University university)
        {
            if (ModelState.IsValid)
            {
                db.Universities.Add(university);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(university);
        }

        // GET: Universities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // POST: Universities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Institute,dateofreporting,Course,Department,year")] University university)
        {
            if (ModelState.IsValid)
            {
                db.Entry(university).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(university);
        }

        // GET: Universities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            University university = db.Universities.Find(id);
            if (university == null)
            {
                return HttpNotFound();
            }
            return View(university);
        }

        // POST: Universities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            University university = db.Universities.Find(id);
            db.Universities.Remove(university);
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
