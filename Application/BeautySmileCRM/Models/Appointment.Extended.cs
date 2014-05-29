using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Models
{
    public partial class Appointment
    {
        public string Title
        {
            get 
            {
                return String.Format("{0:dd.MM.yyyy HH:mm} - {1:dd.MM.yyyy HH:mm}", StartTime, EndTime);
            }
        }
    }
}
