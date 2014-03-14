using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Models
{
    public partial class FinancialTransaction
    {
		public decimal SignedAmount
        {
            get { return TransactionTypeID == (int)Enums.TransactionType.Withdrawal ? -Amount : Amount; }
        }
    }
}
