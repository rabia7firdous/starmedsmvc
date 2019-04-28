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
            cartProducts.Addresses = new List<Address>();
            cartProducts.TotalItems = 0;
            cartProducts.TotalPrice = 0;

            int UserId = 0;
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserID"]);
            }

            //get products from cart table
            var products = db.Carts.Where(c => c.userid == UserId).ToList();
            foreach (var product in products)
            {
                cartProducts.TotalItems++;
                if (product.producttype == "healthproduct")
                {
                    var prod = db.Products.Where(i => i.Product_Id == product.productid).FirstOrDefault();
                    cartProducts.HealthProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.Product_Price);
                }
                else if (product.producttype == "pharmacyproduct")
                {
                    var prod = db.PharmacyProducts.Where(i => i.ProductId == product.productid).FirstOrDefault();
                    cartProducts.PharmacyProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.ProductPrice);
                }
            }

            //get adresses from adress table
            var addresses = db.Addresses.Where(c => c.UserId == UserId).ToList();
            cartProducts.Addresses = addresses.Select(i => new Address()
            {
                AddressId = i.AddressId,
                AddressDoorNo = i.AddressDoorNo,
                AddressLine1 = i.AddressLine1,
                AddressLine2 = i.AddressLine2,
                LandMark = i.LandMark,
                PinCode = i.PinCode,
                UserId = i.UserId,
                Name = i.Name,
                City = i.City,
                State = i.State
            }).ToList(); ;
            Session["count"] = db.Carts.Where(c => c.userid == UserId).Count();
            return View("FirstStep", cartProducts);

        }


        [HttpPost]
        public ActionResult FinalStep(string customeraddress)
        {

            CartProductModel cartProducts = new CartProductModel();
            cartProducts.HealthProducts = new List<Product>();
            cartProducts.PharmacyProducts = new List<PharmacyProduct>();
            cartProducts.Addresses = new List<Address>();
            cartProducts.TotalItems = 0;
            cartProducts.TotalPrice = 0;

            int UserId = 0;
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserID"]);
            }

            //get products from cart table
            var products = db.Carts.Where(c => c.userid == UserId).ToList();
            foreach (var product in products)
            {
                cartProducts.TotalItems++;
                if (product.producttype == "healthproduct")
                {
                    var prod = db.Products.Where(i => i.Product_Id == product.productid).FirstOrDefault();
                    cartProducts.HealthProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.Product_Price);
                }
                else if (product.producttype == "pharmacyproduct")
                {
                    var prod = db.PharmacyProducts.Where(i => i.ProductId == product.productid).FirstOrDefault();
                    cartProducts.PharmacyProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.ProductPrice);
                }
            }

            //get adresses from adress table
            var addresses = db.Addresses.Where(c => c.UserId == UserId).ToList();
            cartProducts.Addresses = addresses.Select(i => new Address()
            {
                AddressId = i.AddressId,
                AddressDoorNo = i.AddressDoorNo,
                AddressLine1 = i.AddressLine1,
                AddressLine2 = i.AddressLine2,
                LandMark = i.LandMark,
                PinCode = i.PinCode,
                UserId = i.UserId,
                Name = i.Name,
                City = i.City,
                State = i.State
            }).ToList(); ;
            Session["count"] = db.Carts.Where(c => c.userid == UserId).Count();
            if (customeraddress == null)
            {
                ViewBag.message = "Select Address";
                return View("SecondStep", cartProducts);
            }
            else
            {
                Session["SelectedAddress"] = customeraddress;
                return View("FinalStep", cartProducts);
            }

        }

        [HttpPost]
        public ActionResult PlaceOrder(string paymentType)
        {
            int addressId = Convert.ToInt32(System.Web.HttpContext.Current.Session["SelectedAddress"]);
            CartProductModel cartProducts = new CartProductModel();
            cartProducts.HealthProducts = new List<Product>();
            cartProducts.PharmacyProducts = new List<PharmacyProduct>();
            cartProducts.Addresses = new List<Address>();
            cartProducts.TotalItems = 0;
            cartProducts.TotalPrice = 0;

            int UserId = 0;
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserID"]);
            }

            //get products from cart table
            var products = db.Carts.Where(c => c.userid == UserId).ToList();
            foreach (var product in products)
            {
                cartProducts.TotalItems++;
                if (product.producttype == "healthproduct")
                {
                    var prod = db.Products.Where(i => i.Product_Id == product.productid).FirstOrDefault();
                    cartProducts.HealthProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.Product_Price);
                }
                else if (product.producttype == "pharmacyproduct")
                {
                    var prod = db.PharmacyProducts.Where(i => i.ProductId == product.productid).FirstOrDefault();
                    cartProducts.PharmacyProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.ProductPrice);
                }
            }

            //get adresses from adress table
            var addresses = db.Addresses.Where(c => c.UserId == UserId).ToList();
            cartProducts.Addresses = addresses.Select(i => new Address()
            {
                AddressId = i.AddressId,
                AddressDoorNo = i.AddressDoorNo,
                AddressLine1 = i.AddressLine1,
                AddressLine2 = i.AddressLine2,
                LandMark = i.LandMark,
                PinCode = i.PinCode,
                UserId = i.UserId,
                Name = i.Name,
                City = i.City,
                State = i.State
            }).ToList(); ;
            Session["count"] = db.Carts.Where(c => c.userid == UserId).Count();
            if (paymentType == null)
            {
                ViewBag.message = "Select Payment Method";
                return View("FinalStep", cartProducts);
            }
            else
            {
                if (paymentType == "cod")
                {
                    orderPlacedProduct orderpp = new orderPlacedProduct();
                    string orderedhp_products = string.Empty;
                    foreach (var item in cartProducts.HealthProducts)
                    {
                        if (string.IsNullOrEmpty(orderedhp_products))
                        {
                            orderedhp_products = item.Product_Id.ToString();
                        }
                        else
                        {
                            orderedhp_products = orderedhp_products + "|" + item.Product_Id.ToString();
                        }

                    }

                    string orderedpharm_products = string.Empty;
                    foreach (var item in cartProducts.PharmacyProducts)
                    {
                        if (string.IsNullOrEmpty(orderedpharm_products))
                        {
                            orderedpharm_products = item.ProductId.ToString();
                        }
                        else
                        {
                            orderedpharm_products = orderedpharm_products + "|" + item.ProductId.ToString();
                        }
                    }
                    orderpp.orderPlacedHealthProducts = orderedhp_products;
                    orderpp.orderPlacedPharmacyProducts = orderedpharm_products;
                    db.orderPlacedProducts.Add(orderpp);
                    db.SaveChanges();

                    int orderPlacedId = orderpp.orderPlacedId;

                    Order order = new Order();
                    order.orderdate = DateTime.Now.Date;
                    order.orderstatus = "In Progress";
                    order.userid = UserId;
                    order.addressid = addressId;
                    order.paymenttype = paymentType;
                    if (paymentType == "online")
                    {
                        order.paymentstatus = "complete";
                    }
                    else
                    {
                        order.paymentstatus = "In Progress";
                    }
                    order.totalamount = cartProducts.TotalPrice;
                    order.orderPlacedId = orderPlacedId;
                    order.totalItems = cartProducts.TotalItems;
                    db.Orders.Add(order);
                    db.SaveChanges();


                    var cartproducts = db.Carts.Where(i => i.userid == UserId).ToList();
                    foreach (var item in cartproducts)
                    {
                        db.Carts.Remove(item);
                        db.SaveChanges();
                    }
                }
                else
                {
                    orderPlacedProduct orderpp = new orderPlacedProduct();
                    string orderedhp_products = string.Empty;
                    foreach (var item in cartProducts.HealthProducts)
                    {
                        if (string.IsNullOrEmpty(orderedhp_products))
                        {
                            orderedhp_products = item.Product_Id.ToString();
                        }
                        else
                        {
                            orderedhp_products = orderedhp_products + "|" + item.Product_Id.ToString();
                        }

                    }

                    string orderedpharm_products = string.Empty;
                    foreach (var item in cartProducts.PharmacyProducts)
                    {
                        if (string.IsNullOrEmpty(orderedpharm_products))
                        {
                            orderedpharm_products = item.ProductId.ToString();
                        }
                        else
                        {
                            orderedpharm_products = orderedpharm_products + "|" + item.ProductId.ToString();
                        }
                    }
                    orderpp.orderPlacedHealthProducts = orderedhp_products;
                    orderpp.orderPlacedPharmacyProducts = orderedpharm_products;
                    db.orderPlacedProducts.Add(orderpp);
                    db.SaveChanges();

                    int orderPlacedId = orderpp.orderPlacedId;

                    Order order = new Order();
                    order.orderdate = DateTime.Now.Date;
                    order.orderstatus = "In Progress";
                    order.userid = UserId;
                    order.addressid = addressId;
                    order.paymenttype = paymentType;
                    if (paymentType == "online")
                    {
                        order.paymentstatus = "complete";
                    }
                    else
                    {
                        order.paymentstatus = "In Progress";
                    }
                    order.totalamount = cartProducts.TotalPrice;
                    order.orderPlacedId = orderPlacedId;
                    order.totalItems = cartProducts.TotalItems;
                    db.Orders.Add(order);
                    db.SaveChanges();
                    var cartproducts = db.Carts.Where(i => i.userid == UserId).ToList();
                    foreach (var item in cartproducts)
                    {
                        db.Carts.Remove(item);
                        db.SaveChanges();
                    }

                    
                }



                return View("OrderPlaced");
            }
        }

        [HttpGet]
        public ActionResult SecondStep()
        {
            CartProductModel cartProducts = new CartProductModel();
            cartProducts.HealthProducts = new List<Product>();
            cartProducts.PharmacyProducts = new List<PharmacyProduct>();
            cartProducts.Addresses = new List<Address>();
            cartProducts.TotalItems = 0;
            cartProducts.TotalPrice = 0;

            int UserId = 0;
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserID"]);
            }

            //get products from cart table
            var products = db.Carts.Where(c => c.userid == UserId).ToList();
            foreach (var product in products)
            {
                cartProducts.TotalItems++;
                if (product.producttype == "healthproduct")
                {
                    var prod = db.Products.Where(i => i.Product_Id == product.productid).FirstOrDefault();
                    cartProducts.HealthProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.Product_Price);
                }
                else if (product.producttype == "pharmacyproduct")
                {
                    var prod = db.PharmacyProducts.Where(i => i.ProductId == product.productid).FirstOrDefault();
                    cartProducts.PharmacyProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.ProductPrice);
                }
            }

            //get adresses from adress table
            var addresses = db.Addresses.Where(c => c.UserId == UserId).ToList();
            cartProducts.Addresses = addresses.Select(i => new Address()
            {
                AddressId = i.AddressId,
                AddressDoorNo = i.AddressDoorNo,
                AddressLine1 = i.AddressLine1,
                AddressLine2 = i.AddressLine2,
                LandMark = i.LandMark,
                PinCode = i.PinCode,
                UserId = i.UserId,
                Name = i.Name,
                City = i.City,
                State = i.State
            }).ToList(); ;
            Session["count"] = db.Carts.Where(c => c.userid == UserId).Count();
            return View("SecondStep", cartProducts);

        }

        [HttpGet]
        public ActionResult FinalStep()
        {
            CartProductModel cartProducts = new CartProductModel();
            cartProducts.HealthProducts = new List<Product>();
            cartProducts.PharmacyProducts = new List<PharmacyProduct>();
            cartProducts.Addresses = new List<Address>();
            cartProducts.TotalItems = 0;
            cartProducts.TotalPrice = 0;

            int UserId = 0;
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                UserId = Convert.ToInt32(Session["UserID"]);
            }

            //get products from cart table
            var products = db.Carts.Where(c => c.userid == UserId).ToList();
            foreach (var product in products)
            {
                cartProducts.TotalItems++;
                if (product.producttype == "healthproduct")
                {
                    var prod = db.Products.Where(i => i.Product_Id == product.productid).FirstOrDefault();
                    cartProducts.HealthProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.Product_Price);
                }
                else if (product.producttype == "pharmacyproduct")
                {
                    var prod = db.PharmacyProducts.Where(i => i.ProductId == product.productid).FirstOrDefault();
                    cartProducts.PharmacyProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.ProductPrice);
                }
            }

            //get adresses from adress table
            var addresses = db.Addresses.Where(c => c.UserId == UserId).ToList();
            cartProducts.Addresses = addresses.Select(i => new Address()
            {
                AddressId = i.AddressId,
                AddressDoorNo = i.AddressDoorNo,
                AddressLine1 = i.AddressLine1,
                AddressLine2 = i.AddressLine2,
                LandMark = i.LandMark,
                PinCode = i.PinCode,
                UserId = i.UserId,
                Name = i.Name,
                City = i.City,
                State = i.State
            }).ToList(); ;
            Session["count"] = db.Carts.Where(c => c.userid == UserId).Count();
            return View("FinalStep", cartProducts);

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
            cartProducts.TotalItems = 0;
            cartProducts.TotalPrice = 0;

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
            if (cartprod != null)
            {
                db.Carts.Remove(cartprod);
                db.SaveChanges();
            }

            var products = db.Carts.Where(c => c.userid == UserId).ToList();
            foreach (var product in products)
            {
                cartProducts.TotalItems++;
                if (product.producttype == "healthproduct")
                {
                    var prod = db.Products.Where(i => i.Product_Id == product.productid).FirstOrDefault();
                    cartProducts.HealthProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.Product_Price);
                }
                else if (product.producttype == "pharmacyproduct")
                {
                    var prod = db.PharmacyProducts.Where(i => i.ProductId == product.productid).FirstOrDefault();
                    cartProducts.PharmacyProducts.Add(prod);
                    cartProducts.TotalPrice = cartProducts.TotalPrice + Convert.ToInt32(prod.ProductPrice);
                }
            }

            //get adresses from adress table
            var addresses = db.Addresses.Where(c => c.UserId == UserId).ToList();
            cartProducts.Addresses = addresses.Select(i => new Address()
            {
                AddressId = i.AddressId,
                AddressDoorNo = i.AddressDoorNo,
                AddressLine1 = i.AddressLine1,
                AddressLine2 = i.AddressLine2,
                LandMark = i.LandMark,
                PinCode = i.PinCode,
                UserId = i.UserId,
                Name = i.Name,
                City = i.City,
                State = i.State
            }).ToList(); ;

            Session["count"] = db.Carts.Where(c => c.userid == UserId).Count();
            return View("FirstStep", cartProducts);
        }
    }
}