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
    public class PharmacySubCategoriesController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();

        // GET: PharmacySubCategories
        public ActionResult Index()
        {
            var pharmacySubCategories = db.PharmacySubCategories.Include(p => p.PharmacyCategory);
            return View(pharmacySubCategories.ToList());
        }

        // GET: PharmacySubCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacySubCategory pharmacySubCategory = db.PharmacySubCategories.Find(id);
            if (pharmacySubCategory == null)
            {
                return HttpNotFound();
            }
            return View(pharmacySubCategory);
        }

        // GET: PharmacySubCategories/Create
        public ActionResult Create()
        {
            ViewBag.PharmacyCategoryId = new SelectList(db.PharmacyCategories, "PharmacyCategoryId", "PharmacyCategoryName");
            return View();
        }

        // POST: PharmacySubCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PharmacySubCatId,PharmacySubCatName,PharmacyCategoryId,PharmacySubCatImage")] PharmacySubCategory pharmacySubCategory)
        {
            if (ModelState.IsValid)
            {
                db.PharmacySubCategories.Add(pharmacySubCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PharmacyCategoryId = new SelectList(db.PharmacyCategories, "PharmacyCategoryId", "PharmacyCategoryName", pharmacySubCategory.PharmacyCategoryId);
            return View(pharmacySubCategory);
        }

        // GET: PharmacySubCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacySubCategory pharmacySubCategory = db.PharmacySubCategories.Find(id);
            if (pharmacySubCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.PharmacyCategoryId = new SelectList(db.PharmacyCategories, "PharmacyCategoryId", "PharmacyCategoryName", pharmacySubCategory.PharmacyCategoryId);
            return View(pharmacySubCategory);
        }

        // POST: PharmacySubCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PharmacySubCatId,PharmacySubCatName,PharmacyCategoryId,PharmacySubCatImage")] PharmacySubCategory pharmacySubCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pharmacySubCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PharmacyCategoryId = new SelectList(db.PharmacyCategories, "PharmacyCategoryId", "PharmacyCategoryName", pharmacySubCategory.PharmacyCategoryId);
            return View(pharmacySubCategory);
        }

        // GET: PharmacySubCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacySubCategory pharmacySubCategory = db.PharmacySubCategories.Find(id);
            if (pharmacySubCategory == null)
            {
                return HttpNotFound();
            }
            return View(pharmacySubCategory);
        }

        // POST: PharmacySubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PharmacySubCategory pharmacySubCategory = db.PharmacySubCategories.Find(id);
            db.PharmacySubCategories.Remove(pharmacySubCategory);
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
