using DevExpress.Xpf.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using BeautySmileCRM.Enums;
using DevExpress.Xpf.Core.ServerMode;
using Ext = SmartClasses;
using BeautySmileCRM.ViewModels.Base;
using Microsoft.Win32;
using DevExpress.Xpf.Grid;

namespace BeautySmileCRM.ViewModels
{
    public class VisitHistory : BaseNavigationViewModel
    {
        private readonly Models.CRMContext _dc;
        private Models.AppointmentView _selectedAppointment;

        public Models.AppointmentView SelectedAppointment
        {
            get { return _selectedAppointment; }
            set
            {
                if(_selectedAppointment != value)
                {
                    _selectedAppointment = value;
                    RaisePropertiesChanged("SelectedAppointment");
                }
            }
        }
        public LinqInstantFeedbackDataSource DataSource { get; private set; }
        
        public IDictionary<int, string> Clients
        {
            get
            {
                IDictionary<int, string> result = null;
                using(var dc = new Models.CRMContext())
                {
                    result = _dc.Customers.ToDictionary(x => x.ID, y => y.ShortName);
                };
                return result;
            }
        }
        public IDictionary<int, string> Staff
        {
            get
            {
                IDictionary<int, string> result = null;
                using (var dc = new Models.CRMContext())
                {
                    result = _dc.Staffs.ToDictionary(x => x.ID, y => y.ShortName);
                };
                return result;
            }
        }
        public IDictionary<int, string> Users
        {
            get
            {
                IDictionary<int, string> result = null;
                using (var dc = new Models.CRMContext())
                {
                    result = _dc.Users.ToDictionary(x => x.ID, y => y.Login);
                };
                return result;
            }
        }
        public IDictionary<int, string> States
        {
            get
            {
                return Ext.Utils.EnumUtils.ToDescriptionDictionary<Enums.AppointmentState>();
            }
        }        
        

        public ICommand RefreshCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }


        public VisitHistory()
        {
            _dc = new Models.CRMContext();
            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);
            ExportCommand = new DelegateCommand<object>(onExportCommandExecute);
            initDataSource();
        }
        private void initDataSource()
        {
            DataSource = new LinqInstantFeedbackDataSource()
            {
                QueryableSource = _dc.AppointmentView,
                KeyExpression = "AppointmentID",
                DefaultSorting = "AppointmentID ASC"
            };
            RaisePropertyChanged("DataSource");
        }
        private void onRefreshCommandExecute()
        {
            DataSource.Refresh();
        }
        private void onExportCommandExecute(object param)
        {
            var table = param as TableView;

            var dlg = new SaveFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = "xlsx",
                FileName = "История визитов",
                Filter = "Файлы MS Excel 2007-2013|*.xlsx"
            };

            if (dlg.ShowDialog() == true)
            {
                table.ExportToXlsx(dlg.FileName);
            };
        }
    }
}
