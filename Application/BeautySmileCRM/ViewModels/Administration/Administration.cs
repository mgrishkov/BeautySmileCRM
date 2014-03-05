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
        private AdminEdit _viewModel;

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

        }
        private void onAddUserCommandExecute()
        {

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
            if (_viewModel == null)
                _viewModel = new AdminEdit();

            _viewModel.Mode = mode;

            var saveCommand = new UICommand()
            {
                Id = MessageBoxResult.OK,
                Caption = "Сохранить",
                IsCancel = false,
                IsDefault = true,
                Command = new DelegateCommand<CancelEventArgs>(
                    x => { },
                    x => { return _viewModel.AllowSave; }
                ),
            };
            var cancelCommand = new UICommand()
            {
                Id = MessageBoxResult.Cancel,
                Caption = "Отменить",
                IsCancel = true,
                IsDefault = false,
                Command = new DelegateCommand<CancelEventArgs>(onDialogCancelCommandtExecuting)
            };

            var result = DialogService.ShowDialog(
                dialogCommands: new List<UICommand>() { saveCommand, cancelCommand },
                title: _viewModel.FullTitle,
                viewModel: _viewModel
            );
        }
        private void onDialogCancelCommandtExecuting(CancelEventArgs parameter)
        {
            MessageService.Show(messageBoxText: "Want to save your changes?", 
                caption: "Document", 
                button: MessageBoxButton.YesNoCancel);
        }
    }
}

