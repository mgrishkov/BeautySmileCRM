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

namespace BeautySmileCRM.ViewModels
{
    public class Discount : BaseNavigationViewModel
    {
        protected IDialogService DialogService { get { return GetService<IDialogService>(); } }
        protected IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }

        private readonly Models.CRMContext _dc;
        private Models.CumulativeDiscount _selectedDiscount;

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
        public LinqInstantFeedbackDataSource DataSource { get; private set; }

        public ICommand RefreshCommand { get; private set; }
        public ICommand AddDiscountCommand { get; private set; }
        public ICommand EditDiscountCommand { get; private set; }
        public ICommand DeleteDiscountCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }

        public Discount()
        {
            _dc = new Models.CRMContext();
            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);
            AddDiscountCommand = new DelegateCommand(onAddDiscountCommandExecute);
            EditDiscountCommand = new DelegateCommand(onEditDiscountCommandExecute,
                () => { return SelectedDiscount != null; });
            DeleteDiscountCommand = new DelegateCommand(onDeleteDiscountCommandExecute,
                () => { return SelectedDiscount != null; });
            ExportCommand = new DelegateCommand(onExportCommandExecute);
            initDataSource();

        }
        private void initDataSource()
        {
            DataSource = new LinqInstantFeedbackDataSource()
            {
                QueryableSource = _dc.CumulativeDiscounts,
                KeyExpression = "ID",
                DefaultSorting = "Percent ASC"
            };
            RaisePropertyChanged("DataSource");
        }
        private void onRefreshCommandExecute()
        {

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

        }
        private void onExportCommandExecute()
        {

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
            viewModel.ShowEditDialog();
        }
    }
}
