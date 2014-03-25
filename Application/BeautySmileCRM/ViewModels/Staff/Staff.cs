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
using Models = BeautySmileCRM.Models;
using DevExpress.Xpf.Core.ServerMode;
using BeautySmileCRM.ViewModels.Base;
using System.Data.Entity.Infrastructure;
using System.Windows;
using Microsoft.Win32;
using DevExpress.Xpf.Grid;

namespace BeautySmileCRM.ViewModels
{
    public class Staff : BaseNavigationViewModel
    {
        protected IDialogService DialogService { get { return GetService<IDialogService>(); } }
        protected IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }

        private Models.CRMContext _dc;
        private Models.Staff _selectedStaff;
        private IEnumerable<Models.Staff> _staffs;

        public Models.Staff SelectedStaff
        {
            get { return _selectedStaff; }
            set
            {
                if(_selectedStaff != value)
                {
                    _selectedStaff = value;
                    RaisePropertiesChanged("SelectedStaff");
                }
            }
        }
        public IEnumerable<Models.Staff> Staffs
        {
            get { return _staffs; }
            set
            {
                if(_staffs != value)
                {
                    _staffs = value;
                    RaisePropertyChanged("Staffs");
                }
            }
        }

        public ICommand RefreshCommand { get; private set; }
        public ICommand AddStaffCommand { get; private set; }
        public ICommand EditStaffCommand { get; private set; }
        public ICommand DeleteStaffCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }

        public Staff()
        {
            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);

            AddStaffCommand = new DelegateCommand(onAddStaffCommandExecute,
                () => { return CurrentUser.HasPrivilege(Privilege.CreateStaff); });

            EditStaffCommand = new DelegateCommand(onEditStaffCommandExecute,
                () => { return SelectedStaff != null && CurrentUser.HasPrivilege(Privilege.ModifyStaff); });

            DeleteStaffCommand = new DelegateCommand(onDeleteStaffCommandExecute,
                () => { return SelectedStaff != null && CurrentUser.HasPrivilege(Privilege.DeleteStaff); });

            ExportCommand = new DelegateCommand<object>(onExportCommandExecute);

            Task.Factory.StartNew(() => refresh());
        }
        private void refresh()
        {
            if (_dc != null)
                _dc.Dispose();

            _dc = new Models.CRMContext();
            Staffs = _dc.Staffs.ToList();
        }
        
        private void onRefreshCommandExecute()
        {
            refresh();
        }
        private void onAddStaffCommandExecute()
        {
            ShowDialog(DialogMode.Create);
        }
        private void onEditStaffCommandExecute()
        {
            ShowDialog(DialogMode.Update);
        }
        private void onDeleteStaffCommandExecute()
        {
            if (MessageService.Show("Вы действительно хотите удалить выбранного сотрудника?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _dc.Staffs.Remove(SelectedStaff);
                    _dc.SaveChanges();
                    refresh();
                }
                catch
                {
                    MessageService.Show(String.Format("Сотрудниу {0} не может быть удален, т.к. имеются связанные с ним визиты.\nДля запрета выбора данного сотрудника при создании или редактировнаии визита, установите дату его увольнентя в форме редактирования.", SelectedStaff.ShortName), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
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
                FileName = "Сотрудники",
                Filter = "Файлы MS Excel 2007-2013|*.xlsx"
            };

            if (dlg.ShowDialog() == true)
            {
                table.ExportToXlsx(dlg.FileName);
            };
        }


        private void ShowDialog(DialogMode mode)
        {
            StaffEdit viewModel = null;
            switch (mode)
            {
                case DialogMode.Create:
                    viewModel = new StaffEdit(mode, DialogService, MessageService);
                    break;
                case DialogMode.Update:
                case DialogMode.View:
                    viewModel = new StaffEdit(mode, SelectedStaff.ID, DialogService, MessageService);
                    break;
            }
            if(viewModel.ShowEditDialog() == MessageBoxResult.OK)
            {
                refresh();
            }
        }
    }
}
