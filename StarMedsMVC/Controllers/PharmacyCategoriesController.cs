using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StarMedsMVC;
using StarMedsMVC.Models;

namespace StarMedsMVC.Controllers
{
    public class PharmacyCategoriesController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();

        // GET: PharmacyCategories
        public ActionResult Index()
        {
            return PartialView(db.PharmacyCategories.ToList());
        }

        // GET: PharmacyCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacyCategory pharmacyCategory = db.PharmacyCategories.Find(id);
            if (pharmacyCategory == null)
            {
                return HttpNotFound();
            }
            return View(pharmacyCategory);
        }

        // GET: PharmacyCategories/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: PharmacyCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PharmacyCategoryId,PharmacyCategoryName")] PharmacyCategory pharmacyCategory)
        {
            if (ModelState.IsValid)
            {
                db.PharmacyCategories.Add(pharmacyCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return PartialView(pharmacyCategory);
        }

        // GET: PharmacyCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacyCategory pharmacyCategory = db.PharmacyCategories.Find(id);
            if (pharmacyCategory == null)
            {
                return HttpNotFound();
            }
            return PartialView(pharmacyCategory);
        }

        // POST: PharmacyCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PharmacyCategoryId,PharmacyCategoryName")] PharmacyCategory pharmacyCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pharmacyCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return PartialView(pharmacyCategory);
        }

        // GET: PharmacyCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacyCategory pharmacyCategory = db.PharmacyCategories.Find(id);
            if (pharmacyCategory == null)
            {
                return HttpNotFound();
            }
            return View(pharmacyCategory);
        }

        // POST: PharmacyCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PharmacyCategory pharmacyCategory = db.PharmacyCategories.Find(id);
            db.PharmacyCategories.Remove(pharmacyCategory);
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
