using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarMedsMVC.Controllers
{
    public class CartController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Myorder()
        {

            return View(Session["cart"]);

        }  

        public ActionResult AddHealthProducts(int? id)
        {
            
            Product product = db.Products.Find(id);            
            if (Session["cart"] == null)
            {
                List<Product> li = new List<Product>();
                li.Add(product);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = 1;
            }
            else
            {
                List<Product> li = (List<Product>)Session["cart"];
                li.Add(product);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            return RedirectToAction("Index", "Home");
        }


        public ActionResult AddPharmacyProducts(int? id)
        {
            PharmacyProduct product = db.PharmacyProducts.Find(id);
            if (Session["cart"] == null)
            {
                List<PharmacyProduct> li = new List<PharmacyProduct>();
                li.Add(product);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = 1;
            }
            else
            {
                List<PharmacyProduct> li = (List<PharmacyProduct>)Session["cart"];
                li.Add(product);
                Session["cart"] = li;
                ViewBag.cart = li.Count();
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            return RedirectToAction("Index", "Home");
        } 
    }
}