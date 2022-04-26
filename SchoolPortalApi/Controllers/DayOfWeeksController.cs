using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolPortalApi;

namespace SchoolPortalApi.Controllers
{
    public class DayOfWeeksController : Controller
    {
        private SchoolPortalAPIEntities1 db = new SchoolPortalAPIEntities1();

        // GET: DayOfWeeks
        public ActionResult Index()
        {
            return View(db.DayOfWeek.ToList());
        }

        // GET: DayOfWeeks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayOfWeek dayOfWeek = db.DayOfWeek.Find(id);
            if (dayOfWeek == null)
            {
                return HttpNotFound();
            }
            return View(dayOfWeek);
        }

        // GET: DayOfWeeks/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DayOfWeeks/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name")] DayOfWeek dayOfWeek)
        {
            if (ModelState.IsValid)
            {
                db.DayOfWeek.Add(dayOfWeek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dayOfWeek);
        }

        // GET: DayOfWeeks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayOfWeek dayOfWeek = db.DayOfWeek.Find(id);
            if (dayOfWeek == null)
            {
                return HttpNotFound();
            }
            return View(dayOfWeek);
        }

        // POST: DayOfWeeks/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в разделе https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name")] DayOfWeek dayOfWeek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dayOfWeek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dayOfWeek);
        }

        // GET: DayOfWeeks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DayOfWeek dayOfWeek = db.DayOfWeek.Find(id);
            if (dayOfWeek == null)
            {
                return HttpNotFound();
            }
            return View(dayOfWeek);
        }

        // POST: DayOfWeeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DayOfWeek dayOfWeek = db.DayOfWeek.Find(id);
            db.DayOfWeek.Remove(dayOfWeek);
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
