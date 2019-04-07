//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StarMedsMVC
{
    using System;
    using System.Collections.Generic;
    
    public partial class PharmacyProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public byte[] ProductImage { get; set; }
        public string ProductDetails { get; set; }
        public int PharmacySubCatId { get; set; }
        public System.DateTime ExpiryDate { get; set; }
        public string Quantity { get; set; }
        public string ProductType { get; set; }
        public string Manufacturer { get; set; }
    
        public virtual PharmacySubCategory PharmacySubCategory { get; set; }
    }
}