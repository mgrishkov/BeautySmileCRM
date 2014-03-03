using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Models
{
    public partial class Staff
    {
        public string ShortName
        {
            get { return String.Format("{0} {1}.{2}", LastName, FirstName[0], !String.IsNullOrWhiteSpace(MiddleName) ? String.Format("{0}.", MiddleName[0]) : String.Empty); }
        }
        public string FullName
        {
            get { return String.Format("{0} {1}{2}", LastName, FirstName, !String.IsNullOrWhiteSpace(MiddleName) ? String.Format(" {0}", MiddleName) : String.Empty); }
        }
    }
}
