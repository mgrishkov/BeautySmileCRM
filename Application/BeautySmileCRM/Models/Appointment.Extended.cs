using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Models
{
    public partial class Appointment
    {
        public string ShortPurpose
        {
            get
            {
                return Purpose.Length > 53 ? String.Format("{0}...", Purpose.Substring(0, 47)) : Purpose;
            }
        }
        public string Title
        {
            get 
            {
                return String.Format("{0:dd.MM.yyyy HH:mm} - {1:dd.MM.yyyy HH:mm} {2} ({3})", StartTime, EndTime, ShortPurpose, Staff.ShortName);
            }
        }
    }
}
