using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarMedsMVC.Models
{
    public class OrderPlacedModel
    {
        public List<Product> HealthProducts { get; set; }
        public List<PharmacyProduct> PharmacyProducts { get; set; }
        public Address Address { get; set; }
        public Order Order { get; set; }        
    }   
}