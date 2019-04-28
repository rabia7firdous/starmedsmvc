using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StarMedsMVC;

namespace StarMedsMVC.Areas.admin.Controllers
{
    public class OrdersController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();

        // GET: admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.orderPlacedProduct);
            return PartialView(orders.ToList());
        }

        public ActionResult EditIndex()
        {
            var orders = db.Orders.Include(o => o.orderPlacedProduct);
            return PartialView(orders.ToList());
        }

        // GET: admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.orderPlacedId = new SelectList(db.orderPlacedProducts, "orderPlacedId", "orderPlacedHealthProducts");
            return View();
        }

        // POST: admin/Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "orderid,orderdate,orderstatus,userid,addressid,paymenttype,paymentstatus,totalamount,orderPlacedId,totalItems")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.orderPlacedId = new SelectList(db.orderPlacedProducts, "orderPlacedId", "orderPlacedHealthProducts", order.orderPlacedId);
            return View(order);
        }

        // GET: admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.orderPlacedId = new SelectList(db.orderPlacedProducts, "orderPlacedId", "orderPlacedHealthProducts", order.orderPlacedId);
            return View(order);
        }

        // POST: admin/Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "orderid,orderdate,orderstatus,userid,addressid,paymenttype,paymentstatus,totalamount,orderPlacedId,totalItems")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.orderPlacedId = new SelectList(db.orderPlacedProducts, "orderPlacedId", "orderPlacedHealthProducts", order.orderPlacedId);
            return View(order);
        }

        // GET: admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
