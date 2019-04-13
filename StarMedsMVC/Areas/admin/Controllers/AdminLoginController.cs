using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarMedsMVC.Areas.admin.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: admin/AdminLogin
        public ActionResult Index()
        {            
            return View();
        }
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CustomerDetail objUser)
        {
            if (ModelState.IsValid)
            {
                using (starmedsdbEntities db = new starmedsdbEntities())
                {
                    var obj = db.Admins.Where(a => a.AdminUserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["AdminId"] = obj.AdminId.ToString();
                        Session["AdminUserName"] = obj.AdminUserName.ToString();
                        Session["AdminName"] = obj.AdminName.ToString();
                        return RedirectToAction("Index", "DashBoard");
                    }
                }
            }
            return View(objUser);
        }
    }
}