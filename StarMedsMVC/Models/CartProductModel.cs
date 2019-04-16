using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarMedsMVC.Models
{
    public class CartProductModel
    {
        public List<Product> HealthProducts { get; set; }
        public List<PharmacyProduct> PharmacyProducts { get; set; }
        public List<Address> Addresses { get; set; }
        public int TotalItems { get; set; }
        public int TotalPrice { get; set; }
    }
}