using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BeautySmileCRM.Enums;
using DevExpress.Xpf.Mvvm;

namespace BeautySmileCRM.ViewModels
{
    public class Dashboard : BaseNavigationViewModel
    {
        private List<Models.CustomerDashboardData> _clients;
        private List<Models.AppointmentDashboardData> _appointments;
        
        public List<Models.CustomerDashboardData> Clients 
        { 
            get { return _clients; }
            set
            {
                if(_clients != value)
                {
                    _clients = value;
                    RaisePropertiesChanged("Clients");
                }
            }
        }
        public List<Models.AppointmentDashboardData> Appointments
        {
            get { return _appointments; }
            set
            {
                if (_appointments != value)
                {
                    _appointments = value;
                    RaisePropertiesChanged("Appointments");
                }
            }
        }

        public Dashboard()
        {
            using(var dc = new Models.CRMContext())
            {
                Clients = dc.GetCustomerDashboardData().ToList();
                Appointments = dc.GetAppointmentDashboardData().ToList();
            }
        }

    }
}
