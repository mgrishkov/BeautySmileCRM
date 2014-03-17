using System;
using System.ComponentModel;

namespace BeautySmileCRM.Enums
{
    /// <summary>
    /// TransactionType auto generated enumeration
    /// </summary>
    public enum TransactionType
    {
		[Description("Пополнение")]
		Deposit             = 1,
		[Description("Списание")]
		Withdrawal          = 2,
    }        
}