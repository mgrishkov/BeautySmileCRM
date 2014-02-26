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
using Models = BeautySmileCRM.Models;
using DevExpress.Xpf.Core.ServerMode;

namespace BeautySmileCRM.ViewModels
{
    public class Staff : BaseNavigationViewModel
    {
        private readonly Models.CRMContext _dc;
        private Models.Staff _selectedStaff;

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
        public LinqInstantFeedbackDataSource DataSource { get; private set; }

        public ICommand RefreshCommand { get; private set; }
        public ICommand AddStaffCommand { get; private set; }
        public ICommand EditStaffCommand { get; private set; }
        public ICommand DeleteStaffCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }

        public Staff()
        {
            _dc = new Models.CRMContext();
            RefreshCommand = new DelegateCommand(onRefreshCommandExecute);
            AddStaffCommand = new DelegateCommand(onAddStaffCommandExecute);
            EditStaffCommand = new DelegateCommand(onEditStaffCommandExecute,
                () => { return SelectedStaff != null; });
            DeleteStaffCommand = new DelegateCommand(onDeleteStaffCommandExecute,
                () => { return SelectedStaff != null; });
            ExportCommand = new DelegateCommand(onExportCommandExecute);
            initDataSource();

        }
        private void initDataSource()
        {
            DataSource = new LinqInstantFeedbackDataSource()
            {
                QueryableSource = _dc.Staffs,
                KeyExpression = "ID",
                DefaultSorting = "LastName ASC"
            };
            RaisePropertyChanged("DataSource");
        }
        private void onRefreshCommandExecute()
        {

        }
        private void onAddStaffCommandExecute()
        {

        }
        private void onEditStaffCommandExecute()
        {

        }
        private void onDeleteStaffCommandExecute()
        {

        }
        private void onExportCommandExecute()
        {

        }
    }
}
