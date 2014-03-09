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
using BeautySmileCRM.ViewModels.Base;
using System.ComponentModel;
using System.Windows;
using SmartClasses.Extensions;

namespace BeautySmileCRM.ViewModels
{
    public class Administration : BaseNavigationViewModel
    {
        protected IDialogService DialogService { get { return GetService<IDialogService>(); } }
        protected IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }

        private readonly Models.CRMContext _dc;
        private Models.User _selectedUser;

        public Models.User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                if(_selectedUser != value)
                {
                    _selectedUser = value;
                    RaisePropertiesChanged("SelectedUser");
                }
            }
        }
        public LinqInstantFeedbackDataSource DataSource { get; private set; }

        public ICommand RefreshCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand EditUserCommand { get; private set; }
        public ICommand DeleteUserCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }

        public Administration()
        {
            _dc = new Models.CRMContext();
            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);
            AddUserCommand = new DelegateCommand(onAddUserCommandExecute);
            EditUserCommand = new DelegateCommand(onEditUserCommandExecute,
                () => { return SelectedUser != null; });
            DeleteUserCommand = new DelegateCommand(onDeleteUserCommandExecute,
                () => { return SelectedUser != null; });
            ExportCommand = new DelegateCommand(onExportCommandExecute);
            initDataSource();

        }
        private void initDataSource()
        {
            DataSource = new LinqInstantFeedbackDataSource()
            {
                QueryableSource = _dc.Users,
                KeyExpression = "ID",
                DefaultSorting = "Login ASC"
            };
            RaisePropertyChanged("DataSource");
        }
        private void onRefreshCommandExecute()
        {
            DataSource.Refresh();
        }
        private void onAddUserCommandExecute()
        {
            ShowDialog(DialogMode.Update);
        }
        private void onEditUserCommandExecute()
        {
            ShowDialog(DialogMode.Update);
        }
        private void onDeleteUserCommandExecute()
        {

        }
        private void onExportCommandExecute()
        {

        }


        private void ShowDialog(DialogMode mode)
        {
            AdminEdit viewModel = null;
            switch (mode)
            {
                case DialogMode.Create:
                    viewModel = new AdminEdit(mode, DialogService, MessageService);
                    break;
                case DialogMode.Update:
                case DialogMode.View:
                    viewModel = new AdminEdit(mode, SelectedUser.ID, DialogService, MessageService);
                    break;
            }
            viewModel.ShowEditDialog();
        }
    }
}

