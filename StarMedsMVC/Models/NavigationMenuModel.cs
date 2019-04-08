using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StarMedsMVC.Models
{
    public class NavigationMenuModel
    {
        public List<Category> HealthProductCategories { get; set; }
        public List<PharmacyCategory> PharmacyCategories { get; set; }
    }
}