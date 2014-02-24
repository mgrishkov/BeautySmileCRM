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
    
    public partial class Contest
    {
        public Contest()
        {
            this.Contest1 = new HashSet<Contest>();
            this.ContestAccounts = new HashSet<ContestAccount>();
            this.FinancialTransactions = new HashSet<FinancialTransaction>();
            this.Symbols = new HashSet<Symbol>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public int ContestPriceID { get; set; }
        public int State { get; set; }
        public Nullable<int> Type { get; set; }
        public int Periodicity { get; set; }
        public int RewardRule { get; set; }
        public Nullable<int> ContestTemplateID { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public int MinimumParticipants { get; set; }
        public int MaximumParticipant { get; set; }
        public bool AreDemoAccountsAllowed { get; set; }
        public decimal InitialPrizeFund { get; set; }
        public Nullable<int> Duration { get; set; }
        public int Formula { get; set; }
    
        public virtual ContestPrice ContestPrice { get; set; }
        public virtual ICollection<Contest> Contest1 { get; set; }
        public virtual Contest Contest2 { get; set; }
        public virtual ICollection<ContestAccount> ContestAccounts { get; set; }
        public virtual ICollection<FinancialTransaction> FinancialTransactions { get; set; }
        public virtual ICollection<Symbol> Symbols { get; set; }
    }
}
