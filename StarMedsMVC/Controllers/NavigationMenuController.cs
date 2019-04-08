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
        // GET: NavigationMenu
        public ActionResult Index()
        {
            NavigationMenuModel nav = new NavigationMenuModel();
            List<Category> hpcategories =  new List<Category>();
            List<PharmacyCategory> pharmacycategories = new List<PharmacyCategory>();
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