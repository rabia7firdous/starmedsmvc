using StarMedsMVC.Models;
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

        public ActionResult MyCart()
        {
            CartProductModel cartProducts = new CartProductModel();
            cartProducts.HealthProducts = new List<Product>();
            cartProducts.PharmacyProducts = new List<PharmacyProduct>();

            int UserId = 0;
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserID"]);
            }
            var products = db.Carts.Where(c => c.userid == UserId).ToList();
            foreach (var product in products)
            {
                if (product.producttype == "healthproduct")
                {
                    var prod = db.Products.Where(i => i.Product_Id == product.productid).FirstOrDefault();
                    cartProducts.HealthProducts.Add(prod);
                }
                else if (product.producttype == "pharmacyproduct")
                {
                    var prod = db.PharmacyProducts.Where(i => i.ProductId == product.productid).FirstOrDefault();
                    cartProducts.PharmacyProducts.Add(prod);
                }
            }

            return View("Index", cartProducts);

        }

        public ActionResult AddProducts(int? id, string producttype)
        {
            int UserId = 0;
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserID"]);
            }

            if (producttype == "healthproduct")
            {
                Cart cartproduct = new Cart();
                cartproduct.addeddate = DateTime.Now;
                cartproduct.productid = Convert.ToInt32(id);
                cartproduct.producttype = producttype;
                cartproduct.userid = UserId;
                db.Carts.Add(cartproduct);
                db.SaveChanges();
            }
            else if (producttype == "pharmacyproduct")
            {
                Cart cartproduct = new Cart();
                cartproduct.addeddate = DateTime.Now;
                cartproduct.productid = Convert.ToInt32(id);
                cartproduct.producttype = producttype;
                cartproduct.userid = UserId;
                db.Carts.Add(cartproduct);
                db.SaveChanges();
            }
            //Product product = db.Products.Find(id);
            //if (Session["cart"] == null)
            //{
            //    List<Product> li = new List<Product>();
            //    li.Add(product);
            //    Session["cart"] = li;
            //    ViewBag.cart = li.Count();
            //    Session["count"] = 1;
            //}
            //else
            //{
            //    List<Product> li = (List<Product>)Session["cart"];
            //    li.Add(product);
            //    Session["cart"] = li;
            //    ViewBag.cart = li.Count();
            //    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            //}
            Session["count"] = db.Carts.Where(c => c.userid == UserId).Count();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveProducts(int? id, string producttype)
        {
            CartProductModel cartProducts = new CartProductModel();
            cartProducts.HealthProducts = new List<Product>();
            cartProducts.PharmacyProducts = new List<PharmacyProduct>();

            int UserId = 0;
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserID"]);
            }
            var cartprod = db.Carts.Where(i => i.productid == id && i.producttype == producttype && i.userid == UserId).FirstOrDefault();
            if (cartprod!=null)
            {
                db.Carts.Remove(cartprod);
                db.SaveChanges();
            }
            
            var products = db.Carts.Where(c => c.userid == UserId).ToList();
            foreach (var product in products)
            {
                if (product.producttype == "healthproduct")
                {
                    var prod = db.Products.Where(i => i.Product_Id == product.productid).FirstOrDefault();
                    cartProducts.HealthProducts.Add(prod);
                }
                else if (product.producttype == "pharmacyproduct")
                {
                    var prod = db.PharmacyProducts.Where(i => i.ProductId == product.productid).FirstOrDefault();
                    cartProducts.PharmacyProducts.Add(prod);
                }
            }
            return View("Index", cartProducts);
        }
    }
}