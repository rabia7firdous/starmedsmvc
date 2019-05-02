using System.Linq;
using System.Web.Mvc;

namespace StarMedsMVC.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerDetail objUser)
        {

            using (starmedsdbEntities db = new starmedsdbEntities())
            {
                var obj = db.CustomerDetails.Where(a => a.UserName.Equals(objUser.UserName) && a.Password.Equals(objUser.Password)).FirstOrDefault();
                if (obj != null)
                {
                    Session["UserID"] = obj.Id.ToString();
                    Session["UserName"] = obj.UserName.ToString();
                    Session["CustomerName"] = obj.CustomerName.ToString();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.message = "Invalid UserName/Password";
                    return View(objUser);
                }
            }

            return View(objUser);
        }

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}