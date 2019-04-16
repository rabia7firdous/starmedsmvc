using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using StarMedsMVC.Models;

namespace StarMedsMVC.Controllers
{
    public class NavigationMenuController : Controller
    {
        private starmedsdbEntities db = new starmedsdbEntities();
        // GET: NavigationMenu
        public ActionResult Index()
        {
            NavigationMenuModel nav = new NavigationMenuModel();
            List<Category> hpcategories =  new List<Category>();
            List<PharmacyCategory> pharmacycategories = new List<PharmacyCategory>();
            if (Session["UserID"] != null)
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                Session["count"] = db.Carts.Where(c => c.userid == userId).Count();
            }
            using (starmedsdbEntities db = new starmedsdbEntities())
            {
                hpcategories = db.Categories.Include(sc => sc.SubCategories.Select(s => s.SubClassifications)).ToList();
                pharmacycategories = db.PharmacyCategories.Include(sc => sc.PharmacySubCategories).ToList();
                nav.HealthProductCategories = new List<Category>();
                nav.HealthProductCategories.AddRange(hpcategories);
                nav.PharmacyCategories = new List<PharmacyCategory>();
                nav.PharmacyCategories.AddRange(pharmacycategories);
            }
            return PartialView("Index", nav);
        }
    }
}