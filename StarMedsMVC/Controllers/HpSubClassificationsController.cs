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
    public class HpSubClassificationsController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();

        // GET: HpSubClassifications
        public ActionResult Index()
        {
            var subClassifications = db.SubClassifications.Include(s => s.SubCategory);
            return PartialView(subClassifications.ToList());
        }

        // GET: HpSubClassifications/Details/5
        public ActionResult Details(int? id)
        {
            List<SubClassification> subclassifications = new List<SubClassification>();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            subclassifications = db.SubClassifications.Where(i => i.SubCat_Id ==id).ToList();
            if (subclassifications == null)
            {
                return HttpNotFound();
            }
            return View(subclassifications);
        }

        // GET: HpSubClassifications/Create
        public ActionResult Create()
        {
            ViewBag.SubCat_Id = new SelectList(db.SubCategories, "SubCat_Id", "SubCatName");
            return PartialView();
        }

        // POST: HpSubClassifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubClassificationId,SubClassificationName,SubCat_Id", Exclude = "SubClassificationImage")] SubClassification subClassification, HttpPostedFileBase SubClassificationImage)
        {
            if (ModelState.IsValid)
            {
                if (SubClassificationImage != null)
                {
                    if (SubClassificationImage.ContentLength > 0)
                    {
                        byte[] imgBinaryData = new byte[SubClassificationImage.ContentLength];
                        int readresult = SubClassificationImage.InputStream.Read(imgBinaryData, 0, SubClassificationImage.ContentLength);
                        subClassification.SubClassificationImage = imgBinaryData;
                    }
                }
                db.SubClassifications.Add(subClassification);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SubCat_Id = new SelectList(db.SubCategories, "SubCat_Id", "SubCatName", subClassification.SubCat_Id);
            return PartialView(subClassification);
        }

        // GET: HpSubClassifications/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubClassification subClassification = db.SubClassifications.Find(id);
            if (subClassification == null)
            {
                return HttpNotFound();
            }
            ViewBag.SubCat_Id = new SelectList(db.SubCategories, "SubCat_Id", "SubCatName", subClassification.SubCat_Id);
            return PartialView(subClassification);
        }

        // POST: HpSubClassifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubClassificationId,SubClassificationName,SubCat_Id", Exclude = "SubClassificationImage")] SubClassification subClassification, HttpPostedFileBase SubClassificationImage)
        {
            if (ModelState.IsValid)
            {
                if (SubClassificationImage != null)
                {
                    if (SubClassificationImage.ContentLength > 0)
                    {
                        byte[] imgBinaryData = new byte[SubClassificationImage.ContentLength];
                        int readresult = SubClassificationImage.InputStream.Read(imgBinaryData, 0, SubClassificationImage.ContentLength);
                        subClassification.SubClassificationImage = imgBinaryData;
                    }
                }
                db.Entry(subClassification).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SubCat_Id = new SelectList(db.SubCategories, "SubCat_Id", "SubCatName", subClassification.SubCat_Id);
            return PartialView(subClassification);
        }

        // GET: HpSubClassifications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubClassification subClassification = db.SubClassifications.Find(id);
            if (subClassification == null)
            {
                return HttpNotFound();
            }
            return View(subClassification);
        }

        // POST: HpSubClassifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SubClassification subClassification = db.SubClassifications.Find(id);
            db.SubClassifications.Remove(subClassification);
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
