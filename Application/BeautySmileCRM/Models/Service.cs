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
    
    public partial class Service
    {
        public Service()
        {
            this.Staffs = new HashSet<Staff>();
            this.AppointmentDetails = new HashSet<AppointmentDetail>();
        }
    
        public int ID { get; set; }
        public string Description { get; set; }
        public int WorkingMinuts { get; set; }
        public decimal Price { get; set; }
    
        public virtual ICollection<Staff> Staffs { get; set; }
        public virtual ICollection<AppointmentDetail> AppointmentDetails { get; set; }
    }
}
