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
    public class OffersController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();
        // GET: Offers
        public ActionResult Index()
        {
            OffersModel offers = new OffersModel();
            offers.HealthProducts = new List<Product>();
            offers.PharmacyProducts = new List<PharmacyProduct>();

            offers.HealthProducts = db.Products.Include(p => p.SubClassification).ToList();
            offers.PharmacyProducts = db.PharmacyProducts.Include(p => p.PharmacySubCategory).ToList();
            return PartialView(offers);          
        }
    }
}