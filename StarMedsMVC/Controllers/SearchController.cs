using StarMedsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarMedsMVC.Controllers
{     
    public class SearchController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Search/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Search/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        [HttpPost]
        public JsonResult SearchSuggestions(string Prefix)
        {
              List<Product> HealthProducts =new List<Product>();
              List<PharmacyProduct> PharmacyProducts = new List<PharmacyProduct>();
              HealthProducts = db.Products.ToList();
              PharmacyProducts = db.PharmacyProducts.ToList();
          
            //Searching records from list using LINQ query  
              var HpSearchList = (from N in HealthProducts
                            where N.Product_Name.StartsWith(Prefix)
                                  select new { N.Product_Name }).ToList();
              var PharmacySearchList = (from N in PharmacyProducts
                                        where N.ProductName.StartsWith(Prefix)
                                        select new { N.ProductName }).ToList();

              return Json(HpSearchList, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SearchProducts(string searchText)
        {
            SearchModel searchResults = new SearchModel();
            searchResults.SearchText = searchText;
            searchResults.HealthProducts = new List<Product>();
            searchResults.PharmacyProducts = new List<PharmacyProduct>();
            searchResults.HealthProducts = db.Products.Where(i=>i.Product_Name.ToLower().Contains(searchText.ToLower())).ToList();
            searchResults.PharmacyProducts = db.PharmacyProducts.Where(i => i.ProductName.ToLower().Contains(searchText.ToLower())).ToList();
            return View("Index", searchResults);
        }
    }
}
