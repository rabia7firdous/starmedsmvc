using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarMedsMVC.Models
{
    public class SearchModel
    {
        public List<Product> HealthProducts { get; set; }
        public List<PharmacyProduct> PharmacyProducts { get; set; }
        public string SearchText { get; set; }
    }
}