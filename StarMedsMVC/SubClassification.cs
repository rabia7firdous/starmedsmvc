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
    
    public partial class SubClassification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubClassification()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int SubClassificationId { get; set; }
        public string SubClassificationName { get; set; }
        public int SubCat_Id { get; set; }
        public byte[] SubClassificationImage { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
