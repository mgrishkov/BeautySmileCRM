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
using DevExpress.Xpf.Grid;
using Microsoft.Win32;
using System.Data.Entity.Infrastructure;

namespace BeautySmileCRM.ViewModels
{
    public class Administration : BaseNavigationViewModel
    {
        protected IDialogService DialogService { get { return GetService<IDialogService>(); } }
        protected IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }

        private Models.CRMContext _dc;
        private IEnumerable<Models.User> _users;
        private Models.User _selectedUser;


        public IEnumerable<Models.User> Users
        {
            get { return _users; }
            set
            {
                if(_users != value)
                {
                    _users = value;
                    RaisePropertyChanged("Users");
                }
            }
        }
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

        public ICommand RefreshCommand { get; private set; }
        public ICommand AddUserCommand { get; private set; }
        public ICommand EditUserCommand { get; private set; }
        public ICommand DeleteUserCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }

        public Administration()
        {
            _dc = new Models.CRMContext();

            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);

            AddUserCommand = new DelegateCommand(onAddUserCommandExecute,
                () => { return CurrentUser.HasPrivilege(Privilege.CreateUser); });

            EditUserCommand = new DelegateCommand(onEditUserCommandExecute,
                () => { return SelectedUser != null 
                    && CurrentUser.HasPrivilege(Privilege.ModifyUser) 
                    && (SelectedUser.Login != "sysdba" || CurrentUser.HasPrivilege(Privilege.ModifySystemUser)) ; });
            
            DeleteUserCommand = new DelegateCommand(onDeleteUserCommandExecute,
                () => { return SelectedUser != null 
                    && CurrentUser.HasPrivilege(Privilege.DeleteUser)
                    && SelectedUser.Login != "sysdba"; });
            
            ExportCommand = new DelegateCommand<object>(onExportCommandExecute);

            refresh();
        }
        private void refresh()
        {
            Task.Factory.StartNew(() =>
            {
                if (Users != null)
                {
                    var dc = ((IObjectContextAdapter)_dc).ObjectContext;
                    dc.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, Users);
                };

                Users = _dc.Users.ToList();
            });
        }

        private void onRefreshCommandExecute()
        {
            refresh();
        }
        private void onAddUserCommandExecute()
        {
            ShowDialog(DialogMode.Create);
        }
        private void onEditUserCommandExecute()
        {
            ShowDialog(DialogMode.Update);
        }
        private void onDeleteUserCommandExecute()
        {
            if (MessageService.Show("Вы действительно хотите удалить выбранного пользователя?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _dc.Users.Remove(SelectedUser);
                    _dc.SaveChanges();
                    refresh();
                }
                catch
                {
                    MessageService.Show(String.Format("Учетная запись {0} не может быть удалена, т.к. от её имени выполнялись действия в системе.\nДля запрета доступа под выбранной учтной записью, заблокируйтее ее, установив дату блокировки в форме редактирования пользователя", SelectedUser.Login), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
        }
        private void onExportCommandExecute(object param)
        {
            var table = param as TableView;

            var dlg = new SaveFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = "xlsx",
                FileName = "Пользователи системы",
                Filter = "Файлы MS Excel 2007-2013|*.xlsx"
            };

            if (dlg.ShowDialog() == true)
            {
                table.ExportToXlsx(dlg.FileName);
            };
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
            if (viewModel.ShowEditDialog() == MessageBoxResult.OK)
            {
                refresh();
            };
        }
    }
}

