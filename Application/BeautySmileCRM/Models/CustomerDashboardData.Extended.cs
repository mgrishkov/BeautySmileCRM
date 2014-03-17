using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Models
{
    public partial class CustomerDashboardData
    {
        public bool IsNewGroup
        {
            get
            {
                return Convert.ToBoolean(this.NewGroup);
            }
        }
    }
}
