//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BeautySmileCRM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DiscountCard
    {
        public DiscountCard()
        {
            this.Customers = new HashSet<Customer>();
        }
    
        public int ID { get; set; }
        public string Code { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal TotalPurchaseValue { get; set; }
        public int DiscountTypeID { get; set; }
        public decimal MinDiscount { get; set; }
        public decimal MaxDiscount { get; set; }
        public bool FixedDiscount { get; set; }
        public decimal InitialDiscountPercent { get; set; }
    
        public virtual DiscountType DiscountType { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
