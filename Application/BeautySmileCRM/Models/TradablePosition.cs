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
    
    public partial class TradablePosition
    {
        public int ID { get; set; }
        public int TradableAccountID { get; set; }
        public int InstrumentID { get; set; }
        public string InstrumentSymbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal AvgPrice { get; set; }
        public System.DateTime CreationTime { get; set; }
        public Nullable<decimal> CurrentBalance { get; set; }
    
        public virtual TradableAccount TradableAccount { get; set; }
    }
}
