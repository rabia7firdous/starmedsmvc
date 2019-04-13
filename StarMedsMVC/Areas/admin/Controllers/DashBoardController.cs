using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarMedsMVC.Areas.admin.Controllers
{
    public class DashBoardController : Controller
    {
        // GET: admin/DashBoard
        public ActionResult Index()
        {
            if (Session["AdminId"] == null)
            {
                return RedirectToAction("Login", "Login", new { area = "" });
            }
            return View();
        }
    }
}