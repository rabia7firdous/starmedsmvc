using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace StarMedsMVC.Controllers
{
    public class NavigationMenuController : Controller
    {
        // GET: NavigationMenu
        public ActionResult Index()
        {
            List<Category> categories =  new List<Category>();
            using (starmedsdbEntities db = new starmedsdbEntities())
            {
                categories = db.Categories.Include(sc => sc.SubCategories.Select(s => s.SubClassifications)).ToList();                
            }
            return PartialView("Index", categories);
        }
    }
}