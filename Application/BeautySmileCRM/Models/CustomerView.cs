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
    
    public partial class CustomerView
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int Gender { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public Nullable<int> Zip { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public System.DateTime BirthDate { get; set; }
        public decimal MoneyBalance { get; set; }
        public bool NotifyByEmail { get; set; }
        public bool NotifyBySms { get; set; }
        public bool NotifyByPost { get; set; }
        public System.DateTime CreationTime { get; set; }
        public Nullable<System.DateTime> ModificationTime { get; set; }
        public Nullable<int> DiscountcardID { get; set; }
        public string DiscountCardCode { get; set; }
        public Nullable<decimal> DiscountPercent { get; set; }
        public Nullable<System.DateTime> FirstVisit { get; set; }
        public Nullable<System.DateTime> LastVisit { get; set; }
        public Nullable<System.DateTime> NextVisit { get; set; }
    }
}