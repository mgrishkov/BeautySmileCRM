﻿using DevExpress.Xpf.Mvvm;
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
using DevExpress.Xpf.Grid;
using Microsoft.Win32;

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
        public ICommand ExportCommand { get; private set; }
        public ICommand OnClientDoubleClickCommand { get; private set; }

        public Client()
        {
            _dc = new Models.CRMContext();
            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);
            
            ExportCommand = new DelegateCommand<object>(onExportCommandExecute);
            OnClientDoubleClickCommand = new DelegateCommand<RowDoubleClickEventArgs>(onClientDoubleClickCommandExecuted);
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
                FileName = "Справочник клиентов",
                Filter = "Файлы MS Excel 2007-2013|*.xlsx"
            };

            if (dlg.ShowDialog() == true)
            {
                table.ExportToXlsx(dlg.FileName);
            };
        }
        private void onClientDoubleClickCommandExecuted(RowDoubleClickEventArgs e)
        {
            if (SelectedCustomer != null)
                NavigationService.Navigate("ClientPageView", SelectedCustomer.CustomerID, this);
        }
    }
}
