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
using System.Data.Entity.Infrastructure;
using System.Windows;
using Microsoft.Win32;
using DevExpress.Xpf.Grid;

namespace BeautySmileCRM.ViewModels
{
    public class Service : BaseNavigationViewModel
    {
        protected IDialogService DialogService { get { return GetService<IDialogService>(); } }
        protected IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }

        private Models.CRMContext _dc;
        private Models.Service _selectedService;
        private IEnumerable<Models.Service> _services;

        public Models.Service SelectedService
        {
            get { return _selectedService; }
            set
            {
                if(_selectedService != value)
                {
                    _selectedService = value;
                    RaisePropertiesChanged("SelectedService");
                }
            }
        }
        public IEnumerable<Models.Service> Services
        {
            get { return _services; }
            set
            {
                if(_services != value)
                {
                    _services = value;
                    RaisePropertyChanged("Services");
                }
            }
        }

        public ICommand RefreshCommand { get; private set; }
        public ICommand AddServiceCommand { get; private set; }
        public ICommand EditServiceCommand { get; private set; }
        public ICommand DeleteServiceCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }

        public Service()
        {
            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);

            AddServiceCommand = new DelegateCommand(onAddDiscountCommandExecute,
                () => { return CurrentUser.HasPrivilege(Privilege.CreateCumulativeDiscount); });

            EditServiceCommand = new DelegateCommand(onEditDiscountCommandExecute,
                () => { return SelectedService != null && CurrentUser.HasPrivilege(Privilege.ModifyService); });

            DeleteServiceCommand = new DelegateCommand(onDeleteDiscountCommandExecute,
                () => { return SelectedService != null && CurrentUser.HasPrivilege(Privilege.DeleteService); });

            ExportCommand = new DelegateCommand<object>(onExportCommandExecute);

            Task.Factory.StartNew(() => refresh());
        }
        private void refresh()
        {
            if (_dc != null)
                _dc.Dispose();

            _dc = new Models.CRMContext();
            Services = _dc.Services.ToList();
        }

        private void onRefreshCommandExecute()
        {
            refresh();
        }
        private void onAddDiscountCommandExecute()
        {
            ShowDialog(DialogMode.Create);
        }
        private void onEditDiscountCommandExecute()
        {
            ShowDialog(DialogMode.Update);
        }
        private void onDeleteDiscountCommandExecute()
        {
            if (MessageService.Show("Вы действительно хотите удалить выбранную услугу?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _dc.Services.Remove(SelectedService);
                    _dc.SaveChanges();
                    refresh();
                }
                catch
                {
                    MessageService.Show("Услуга не может быть удалена, т.к. на нее есть ссылки.", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
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
                FileName = "Услуги",
                Filter = "Файлы MS Excel 2007-2013|*.xlsx"
            };

            if (dlg.ShowDialog() == true)
            {
                table.ExportToXlsx(dlg.FileName);
            };
        }

        private void ShowDialog(DialogMode mode)
        {
            ServiceEdit viewModel = null;
            switch (mode)
            {
                case DialogMode.Create:
                    viewModel = new ServiceEdit(mode, DialogService, MessageService);
                    break;
                case DialogMode.Update:
                case DialogMode.View:
                    viewModel = new ServiceEdit(mode, SelectedService.ID, DialogService, MessageService);
                    break;
            }
            if(viewModel.ShowEditDialog() == MessageBoxResult.OK)
            {
                refresh();
            }
        }
    }
}
