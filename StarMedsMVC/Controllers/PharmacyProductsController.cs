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
    public class PharmacyProductsController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();

        // GET: PharmacyProducts
        public ActionResult Index()
        {
            var pharmacyProducts = db.PharmacyProducts.Include(p => p.PharmacySubCategory);
            return View(pharmacyProducts.ToList());
        }

        // GET: PharmacyProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacyProduct pharmacyProduct = db.PharmacyProducts.Find(id);
            if (pharmacyProduct == null)
            {
                return HttpNotFound();
            }
            return View(pharmacyProduct);
        }

        // GET: PharmacyProducts/Create
        public ActionResult Create()
        {
            ViewBag.PharmacySubCatId = new SelectList(db.PharmacySubCategories, "PharmacySubCatId", "PharmacySubCatName");
            return View();
        }

        // POST: PharmacyProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,ProductPrice,ProductImage,ProductDetails,PharmacySubCatId,ExpiryDate,Quantity,ProductType,Manufacturer")] PharmacyProduct pharmacyProduct)
        {
            if (ModelState.IsValid)
            {
                db.PharmacyProducts.Add(pharmacyProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PharmacySubCatId = new SelectList(db.PharmacySubCategories, "PharmacySubCatId", "PharmacySubCatName", pharmacyProduct.PharmacySubCatId);
            return View(pharmacyProduct);
        }

        // GET: PharmacyProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacyProduct pharmacyProduct = db.PharmacyProducts.Find(id);
            if (pharmacyProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.PharmacySubCatId = new SelectList(db.PharmacySubCategories, "PharmacySubCatId", "PharmacySubCatName", pharmacyProduct.PharmacySubCatId);
            return View(pharmacyProduct);
        }

        // POST: PharmacyProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductPrice,ProductImage,ProductDetails,PharmacySubCatId,ExpiryDate,Quantity,ProductType,Manufacturer")] PharmacyProduct pharmacyProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pharmacyProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PharmacySubCatId = new SelectList(db.PharmacySubCategories, "PharmacySubCatId", "PharmacySubCatName", pharmacyProduct.PharmacySubCatId);
            return View(pharmacyProduct);
        }

        // GET: PharmacyProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PharmacyProduct pharmacyProduct = db.PharmacyProducts.Find(id);
            if (pharmacyProduct == null)
            {
                return HttpNotFound();
            }
            return View(pharmacyProduct);
        }

        // POST: PharmacyProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PharmacyProduct pharmacyProduct = db.PharmacyProducts.Find(id);
            db.PharmacyProducts.Remove(pharmacyProduct);
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
