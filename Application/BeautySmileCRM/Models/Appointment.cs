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
    
    public partial class Appointment
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int StaffID { get; set; }
        public string Purpose { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal Discount { get; set; }
        public decimal Payment { get; set; }
        public int StateID { get; set; }
        public System.DateTime CreatintTime { get; set; }
        public int CreatedBy { get; set; }
        public Nullable<System.DateTime> ModificationTime { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual AppointmentState AppointmentState { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
