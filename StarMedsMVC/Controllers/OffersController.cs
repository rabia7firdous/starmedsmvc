﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StarMedsMVC.Controllers
{
    public class OffersController : Controller
    {
        // GET: Offers
        public ActionResult Index()
        {
            return PartialView();
        }
    }
}