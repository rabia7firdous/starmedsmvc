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
    public class HpProductsController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();

        // GET: HpProducts
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.SubClassification);
            return PartialView(products.ToList());
        }

        // GET: HpProducts/Details/5
        public ActionResult Details(int? id)
        {
            List<Product> products = new List<Product>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            products = db.Products.Where(i => i.SubClassificationId == id).ToList();
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: HpProducts/Create
        public ActionResult Create()
        {
            ViewBag.SubClassificationId = new SelectList(db.SubClassifications, "SubClassificationId", "SubClassificationName");
            return PartialView();
        }

        // POST: HpProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Product_Id,Product_Name,Product_Price,Product_Details,SubClassificationId,ExpiryDate,Quantity",Exclude = "Product_Image")] Product product, HttpPostedFileBase Product_Image)
        {
            if (ModelState.IsValid)
            {
                if (Product_Image != null)
                {
                    if (Product_Image.ContentLength > 0)
                    {
                        byte[] imgBinaryData = new byte[Product_Image.ContentLength];
                        int readresult = Product_Image.InputStream.Read(imgBinaryData, 0, Product_Image.ContentLength);
                        product.Product_Image = imgBinaryData;
                    }
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubClassificationId = new SelectList(db.SubClassifications, "SubClassificationId", "SubClassificationName", product.SubClassificationId);
            return PartialView(product);
        }

        // GET: HpProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubClassificationId = new SelectList(db.SubClassifications, "SubClassificationId", "SubClassificationName", product.SubClassificationId);
            return PartialView(product);
        }

        // POST: HpProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Product_Id,Product_Name,Product_Price,Product_Details,SubClassificationId,ExpiryDate,Quantity", Exclude = "Product_Image")] Product product, HttpPostedFileBase Product_Image)
        {
            if (ModelState.IsValid)
            {
                if (Product_Image != null)
                {
                    if (Product_Image.ContentLength > 0)
                    {
                        byte[] imgBinaryData = new byte[Product_Image.ContentLength];
                        int readresult = Product_Image.InputStream.Read(imgBinaryData, 0, Product_Image.ContentLength);
                        product.Product_Image = imgBinaryData;
                    }
                }
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubClassificationId = new SelectList(db.SubClassifications, "SubClassificationId", "SubClassificationName", product.SubClassificationId);
            return PartialView(product);
        }

        // GET: HpProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: HpProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: HpProducts/ProductDetails/5
        public ActionResult ProductDetails(int? id)
        {
           Product product = new Product();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product = db.Products.Find(id);
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
