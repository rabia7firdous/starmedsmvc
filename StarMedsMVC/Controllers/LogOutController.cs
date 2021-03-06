﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarMedsMVC.Controllers
{
    public class LogOutController : Controller
    {
        // GET: LogOff
        public ActionResult LogOut()
        {
            //Removes all keys and values from the session-state collection.
            System.Web.HttpContext.Current.Session.Clear();

            //Cancels the current session.
            System.Web.HttpContext.Current.Session.Abandon();

            return RedirectToAction("Index", "Home");
        }
    }
}