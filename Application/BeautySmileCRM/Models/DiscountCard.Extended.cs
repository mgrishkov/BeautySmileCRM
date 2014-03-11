using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Models
{
    public partial class DiscountCard
    {
        public static string GenerateCode()
        {
            var date = DateTime.Now;
            return String.Format("{0:yyyyMMddHHmmss}", date); 
        }
    }
}
