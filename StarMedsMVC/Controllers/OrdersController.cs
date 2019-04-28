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
    public class OrdersController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();

        // GET: Orders
        public ActionResult Index()
        {
            int UserId = 0;
            List<OrderPlacedModel> orderedProducts = new List<OrderPlacedModel>();  

            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserID"]);
            }

            var orders = db.Orders.Include(o => o.orderPlacedProduct).Where(i => i.userid == UserId).ToList();

            foreach (var item in orders)
            {
                OrderPlacedModel orderedProduct = new OrderPlacedModel();
                orderedProduct.HealthProducts = new List<Product>();
                orderedProduct.PharmacyProducts = new List<PharmacyProduct>();
                orderedProduct.Address = new Address();
                orderedProduct.Order = new Order();
                var hpProductsIds = new List<string>();
                var pharmProductsIds = new List<string>();
                if (!string.IsNullOrEmpty(item.orderPlacedProduct.orderPlacedHealthProducts))
                {
                    hpProductsIds = item.orderPlacedProduct.orderPlacedHealthProducts.Split('|').ToList();
                }

                if (!string.IsNullOrEmpty(item.orderPlacedProduct.orderPlacedPharmacyProducts))
                {
                    pharmProductsIds = item.orderPlacedProduct.orderPlacedPharmacyProducts.Split('|').ToList();
                }

                foreach (var hpId in hpProductsIds)
                {
                    var id = Convert.ToInt32(hpId);
                    var prod = db.Products.Where(i => i.Product_Id ==id ).FirstOrDefault();
                    orderedProduct.HealthProducts.Add(prod);                   
                }

                foreach (var pharmId in pharmProductsIds)
                {
                    var id = Convert.ToInt32(pharmId);
                    var prod = db.PharmacyProducts.Where(i => i.ProductId == id).FirstOrDefault();
                    orderedProduct.PharmacyProducts.Add(prod);
                }
                orderedProduct.Address = db.Addresses.Where(i => i.AddressId == item.addressid).FirstOrDefault();
                orderedProduct.Order = item;
                orderedProducts.Add(orderedProduct);
            }

            return View(orderedProducts);
        }

        // GET: Orders/Details/5
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

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.orderPlacedId = new SelectList(db.orderPlacedProducts, "orderPlacedId", "orderPlacedHealthProducts");
            return View();
        }

        // POST: Orders/Create
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

        // GET: Orders/Edit/5
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

        // POST: Orders/Edit/5
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

        // GET: Orders/Delete/5
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

        // POST: Orders/Delete/5
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
