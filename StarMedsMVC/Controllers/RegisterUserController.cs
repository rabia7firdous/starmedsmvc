using StarMedsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarMedsMVC.Controllers
{
    public class RegisterUserController : Controller
    {
        // GET: RegisterUser
        public ActionResult Index()
        {
            return View();
        }

        // GET: RegisterUser/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegisterUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegisterUser/Create
        [HttpPost]
        public ActionResult Create(CustomerDetail collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    using (starmedsdbEntities db = new starmedsdbEntities())
                    {
                        db.CustomerDetails.Add(collection);
                        db.SaveChanges();
                    }
                    ModelState.Clear();
                    ViewBag.SuccessMessage = "User Registered Successfully";                    
                }

                return View("Index", new CustomerDetail());
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            return View();
        }

        // GET: RegisterUser/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RegisterUser/Edit/5
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

        // GET: RegisterUser/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RegisterUser/Delete/5
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
    }
}
