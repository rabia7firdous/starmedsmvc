using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarMedsMVC.Models
{
    public class CartProductModel
    {
        List<Product> HealthProducts { get; set; }
        List<PharmacyProduct> PharmacyProducts { get; set; }
    }
}