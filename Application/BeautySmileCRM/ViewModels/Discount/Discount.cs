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
    public class Discount : BaseNavigationViewModel
    {
        protected IDialogService DialogService { get { return GetService<IDialogService>(); } }
        protected IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }

        private Models.CRMContext _dc;
        private Models.CumulativeDiscount _selectedDiscount;
        private IEnumerable<Models.CumulativeDiscount> _discounts;

        public Models.CumulativeDiscount SelectedDiscount
        {
            get { return _selectedDiscount; }
            set
            {
                if(_selectedDiscount != value)
                {
                    _selectedDiscount = value;
                    RaisePropertiesChanged("SelectedDiscount");
                }
            }
        }
        public IEnumerable<Models.CumulativeDiscount> Discounts
        {
            get { return _discounts; }
            set
            {
                if(_discounts != value)
                {
                    _discounts = value;
                    RaisePropertyChanged("Discounts");
                }
            }
        }

        public ICommand RefreshCommand { get; private set; }
        public ICommand AddDiscountCommand { get; private set; }
        public ICommand EditDiscountCommand { get; private set; }
        public ICommand DeleteDiscountCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }

        public Discount()
        {
            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);

            AddDiscountCommand = new DelegateCommand(onAddDiscountCommandExecute,
                () => { return CurrentUser.HasPrivilege(Privilege.CreateCumulativeDiscount); });

            EditDiscountCommand = new DelegateCommand(onEditDiscountCommandExecute,
                () => { return SelectedDiscount != null && CurrentUser.HasPrivilege(Privilege.ModifyCumulativeDiscount); });

            DeleteDiscountCommand = new DelegateCommand(onDeleteDiscountCommandExecute,
                () => { return SelectedDiscount != null && CurrentUser.HasPrivilege(Privilege.DeleteCumulativeDiscount); });

            ExportCommand = new DelegateCommand<object>(onExportCommandExecute);

            Task.Factory.StartNew(() => refresh());
        }
        private void refresh()
        {
            if (_dc != null)
                _dc.Dispose();

            _dc = new Models.CRMContext();
            Discounts = _dc.CumulativeDiscounts.ToList();
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
            if (MessageService.Show("Вы действительно хотите удалить выбранную скидку?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    _dc.CumulativeDiscounts.Remove(SelectedDiscount);
                    _dc.SaveChanges();
                    refresh();
                }
                catch
                {
                    MessageService.Show(String.Format("Скидка {0} не может быть удалена, т.к. на нее есть ссылки.", _selectedDiscount.Name), "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
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
                FileName = "Скидки",
                Filter = "Файлы MS Excel 2007-2013|*.xlsx"
            };

            if (dlg.ShowDialog() == true)
            {
                table.ExportToXlsx(dlg.FileName);
            };
        }

        private void ShowDialog(DialogMode mode)
        {
            CumulativeDiscountEdit viewModel = null;
            switch (mode)
            {
                case DialogMode.Create:
                    viewModel = new CumulativeDiscountEdit(mode, DialogService, MessageService);
                    break;
                case DialogMode.Update:
                case DialogMode.View:
                    viewModel = new CumulativeDiscountEdit(mode, SelectedDiscount.ID, DialogService, MessageService);
                    break;
            }
            if(viewModel.ShowEditDialog() == MessageBoxResult.OK)
            {
                refresh();
            }
        }
    }
}
