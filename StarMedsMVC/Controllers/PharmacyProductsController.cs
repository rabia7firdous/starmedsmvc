﻿using System;
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
            return PartialView(pharmacyProducts.ToList());
        }

        public ActionResult EditIndex()
        {
            var pharmacyProducts = db.PharmacyProducts.Include(p => p.PharmacySubCategory);
            return PartialView(pharmacyProducts.ToList());
        }

        // GET: PharmacyProducts/Details/5
        public ActionResult Details(int? id)
        {
            List<PharmacyProduct> products = new List<PharmacyProduct>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products = db.PharmacyProducts.Where(i => i.PharmacySubCatId == id).ToList();
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: PharmacyProducts/Create
        public ActionResult Create()
        {
            ViewBag.PharmacySubCatId = new SelectList(db.PharmacySubCategories, "PharmacySubCatId", "PharmacySubCatName");
            return PartialView();
        }

        // POST: PharmacyProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,ProductPrice,ProductDetails,PharmacySubCatId,ExpiryDate,Quantity,ProductType,Manufacturer", Exclude = "ProductImage")] PharmacyProduct pharmacyProduct, HttpPostedFileBase ProductImage)
        {
            if (ModelState.IsValid)
            {
                if (ProductImage != null)
                {
                    if (ProductImage.ContentLength > 0)
                    {
                        byte[] imgBinaryData = new byte[ProductImage.ContentLength];
                        int readresult = ProductImage.InputStream.Read(imgBinaryData, 0, ProductImage.ContentLength);
                        pharmacyProduct.ProductImage = imgBinaryData;
                    }
                }
                db.PharmacyProducts.Add(pharmacyProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PharmacySubCatId = new SelectList(db.PharmacySubCategories, "PharmacySubCatId", "PharmacySubCatName", pharmacyProduct.PharmacySubCatId);
            return PartialView(pharmacyProduct);
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
            return PartialView(pharmacyProduct);
        }

        // POST: PharmacyProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductPrice,ProductDetails,PharmacySubCatId,ExpiryDate,Quantity,ProductType,Manufacturer", Exclude = "ProductImage")] PharmacyProduct pharmacyProduct, HttpPostedFileBase ProductImage)
        {
            if (ModelState.IsValid)
            {
                if (ProductImage != null)
                {
                    if (ProductImage.ContentLength > 0)
                    {
                        byte[] imgBinaryData = new byte[ProductImage.ContentLength];
                        int readresult = ProductImage.InputStream.Read(imgBinaryData, 0, ProductImage.ContentLength);
                        pharmacyProduct.ProductImage = imgBinaryData;
                    }
                }
                db.Entry(pharmacyProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PharmacySubCatId = new SelectList(db.PharmacySubCategories, "PharmacySubCatId", "PharmacySubCatName", pharmacyProduct.PharmacySubCatId);
            return PartialView(pharmacyProduct);
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

        // GET: PharmacyProducts/ProductDetails/5
        public ActionResult ProductDetails(int? id)
        {
            PharmacyProduct product = new PharmacyProduct();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product = db.PharmacyProducts.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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
