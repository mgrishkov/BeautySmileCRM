using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Mvvm;
using BeautySmileCRM.ViewModels.Base;
using System.Windows.Input;
using DevExpress.Xpf.WindowsUI;
using BeautySmileCRM.Enums;

namespace BeautySmileCRM.ViewModels
{
    public class Header : BaseNavigationViewModel
    {
        public ICommand OnCreateNewCustomerCommand { get; private set; }
        public ICommand OnNavigateToCustomersCommand { get; private set; }
        public ICommand OnNavigateToAppointmentsCommand { get; private set; }
        public ICommand OnNavigateToFinancialTransactionsCommand { get; private set; }
        public ICommand OnNavigateToAdministrationCommand { get; private set; }
        public ICommand OnNavigateToStaffCommand { get; private set; }
        public ICommand OnNavigateToDiscountsCommand { get; private set; }

        public Header()
        {
            OnCreateNewCustomerCommand = new DelegateCommand(() => NavigationService.Navigate("ClientPageView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.CreateCustomer); });

            OnNavigateToCustomersCommand = new DelegateCommand(() => NavigationService.Navigate("ClientView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewCustomer); });

            OnNavigateToAppointmentsCommand = new DelegateCommand(() => NavigationService.Navigate("VisitHistory", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewAppointment); });

            OnNavigateToFinancialTransactionsCommand = new DelegateCommand(() => { throw new NotImplementedException(); },
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewFinancialTransaction); });

            OnNavigateToAdministrationCommand = new DelegateCommand(() => NavigationService.Navigate("AdministrationView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewUser); });

            OnNavigateToStaffCommand = new DelegateCommand(() => NavigationService.Navigate("StaffView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.VIewStaff); });

            OnNavigateToDiscountsCommand = new DelegateCommand(() => NavigationService.Navigate("DiscountView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewConfiguration); });
        }
    }
}
