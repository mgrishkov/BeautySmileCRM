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

namespace BeautySmileCRM.ViewModels
{
    public class Client : BaseNavigationViewModel
    {
        private readonly Models.CRMContext _dc;
        private Models.CustomerView _selectedCustomer;

        public Models.CustomerView SelectedCustomer
        {
            get { return _selectedCustomer; }
            set
            {
                if(_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    RaisePropertiesChanged("SelectedCustomer");
                }
            }
        }
        public LinqInstantFeedbackDataSource DataSource { get; private set; }
        
        public IDictionary<int, string> Genders 
        { 
            get
            {
                return Ext.Utils.EnumUtils.ToDescriptionDictionary<Enums.Gender>();
            }
        }

        public ICommand RefreshCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand EditUserCommand { get; private set; }
        public ICommand DeleteUserCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }


        public Client()
        {
            _dc = new Models.CRMContext();
            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);
            AddUserCommand = new DelegateCommand(onAddUserCommandExecute);
            EditUserCommand = new DelegateCommand(onEditUserCommandExecute,
                () => { return SelectedCustomer != null; });
            DeleteUserCommand = new DelegateCommand(onDeleteUserCommandExecute,
                () => { return SelectedCustomer != null; });
            ExportCommand = new DelegateCommand(onExportCommandExecute);
            initDataSource();
        }
        private void initDataSource()
        {
            DataSource = new LinqInstantFeedbackDataSource()
            {
                QueryableSource = _dc.CustomerView,
                KeyExpression = "CustomerID",
                DefaultSorting = "LastName ASC, FirstName ASC, MiddleName ASC, BirthDate ASC"
            };
            RaisePropertyChanged("DataSource");
        }
        private void onRefreshCommandExecute()
        {

        }
        private void onAddUserCommandExecute()
        {

        }
        private void onEditUserCommandExecute()
        {

        }
        private void onDeleteUserCommandExecute()
        {

        }
        private void onExportCommandExecute()
        {

        }
    }
}
