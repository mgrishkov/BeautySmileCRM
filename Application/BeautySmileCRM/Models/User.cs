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
    
    public partial class User
    {
        public User()
        {
            this.CreatedAppointments = new HashSet<Appointment>();
            this.ModifiedAppointments = new HashSet<Appointment>();
            this.Customers = new HashSet<Customer>();
            this.CreatedFinancialTransaction = new HashSet<FinancialTransaction>();
            this.ModifiedFinancialTransaction = new HashSet<FinancialTransaction>();
            this.Staffs = new HashSet<Staff>();
            this.Privileges = new HashSet<Privilege>();
        }
    
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public bool IsSystem { get; set; }
    
        public virtual ICollection<Appointment> CreatedAppointments { get; set; }
        public virtual ICollection<Appointment> ModifiedAppointments { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<FinancialTransaction> CreatedFinancialTransaction { get; set; }
        public virtual ICollection<FinancialTransaction> ModifiedFinancialTransaction { get; set; }
        public virtual ICollection<Staff> Staffs { get; set; }
        public virtual ICollection<Privilege> Privileges { get; set; }
    }
}
