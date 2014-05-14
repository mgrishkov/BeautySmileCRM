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
using DevExpress.Xpf.Bars;
using BeautySmileCRM.Services;

namespace BeautySmileCRM.ViewModels
{
    public class Header : BaseNavigationViewModel
    {
        public ICommand OnNavigateToHomeCommand { get; private set; }
        public ICommand OnCreateNewCustomerCommand { get; private set; }
        public ICommand OnNavigateToCustomersCommand { get; private set; }
        public ICommand OnNavigateToAppointmentsCommand { get; private set; }
        public ICommand OnNavigateToFinancialTransactionsCommand { get; private set; }
        public ICommand OnNavigateToAdministrationCommand { get; private set; }
        public ICommand OnNavigateToStaffCommand { get; private set; }
        public ICommand OnNavigateToDiscountsCommand { get; private set; }
        public ICommand OnNavigateToQuestionnaireCommand { get; private set; }
        public ICommand OnNavigateToServicesCommand { get; private set; }

        public dynamic NavigateToHome
        {
            get
            {
                return new
                {
                    Name = "ДОМОЙ",
                    Tooltip = "Перейти на главную форму..."
                };
            }
        }
        public dynamic CreateNewCustomer
        {
            get
            {
                return new 
                {
                    Name = "НОВЫЙ КЛИЕНТ",
                    Tooltip = OnCreateNewCustomerCommand.CanExecute(null) ? "Перейти на форму создания новго клиента..." : ApplicationService.SufficientAuthority
                };
            }
        }
        public dynamic NavigateToCustomers
        {
            get
            {
                return new
                {
                    Name = "Справочник клиентов",
                    Tooltip = OnNavigateToCustomersCommand.CanExecute(null) ? "Открыть форму справочника клиентов..." : ApplicationService.SufficientAuthority
                };
            }
        }
        public dynamic NavigateToAppointments
        {
            get
            {
                return new
                {
                    Name = "История визитов",
                    Tooltip = OnNavigateToAppointmentsCommand.CanExecute(null) ? "Открыть форму истории визитов..." : ApplicationService.SufficientAuthority
                };
            }
        }
        public dynamic NavigateToFinancialTransactions
        {
            get
            {
                return new
                {
                    Name = "Финансовые операции",
                    Tooltip = OnNavigateToFinancialTransactionsCommand.CanExecute(null) ? "Открыть форму финансовых операций..." : ApplicationService.SufficientAuthority
                };
            }
        }
        public dynamic NavigateToAdministration
        {
            get
            {
                return new
                {
                    Name = "Администрирование",
                    Tooltip = OnNavigateToAdministrationCommand.CanExecute(null) ? "Открыть форму пользователей приложения и разграничения ролей..." : ApplicationService.SufficientAuthority
                };
            }
        }
        public dynamic NavigateToStaff
        {
            get
            {
                return new
                {
                    Name = "Справочник персонала",
                    Tooltip = OnNavigateToStaffCommand.CanExecute(null) ? "Открыть форму справочника персонала..." : ApplicationService.SufficientAuthority
                };
            }
        }
        public dynamic NavigateToDiscounts
        {
            get
            {
                return new
                {
                    Name = "Справочник скидок",
                    Tooltip = OnNavigateToDiscountsCommand.CanExecute(null) ? "Открыть форму справочника скидок..." : ApplicationService.SufficientAuthority
                };
            }
        }

        public dynamic NavigateToQuestionnaire
        {
            get
            {
                return new
                {
                    Name = "Анкета клиента",
                    Tooltip = "Распечатать анкету клиента..."
                };
            }
        }
        public dynamic NavigateToServices
        {
            get
            {
                return new
                {
                    Name = "Справочник услуг и цен",
                    Tooltip = OnNavigateToServicesCommand.CanExecute(null) ? "Открыть форму справочника услуг и цен..." : ApplicationService.SufficientAuthority
                };
            }
        }

        public bool ShowHomeLink
        {
            get
            {
                return OnNavigateToHomeCommand.CanExecute(null);
            }
        }

        public Header()
        {
            OnNavigateToHomeCommand = new DelegateCommand(() => NavigationService.Navigate("DashboardView", null, this),
                () => { return !(NavigationService.Current is BeautySmileCRM.Views.DashboardView); });

            OnCreateNewCustomerCommand = new DelegateCommand(() => NavigationService.Navigate("ClientPageView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.CreateCustomer); });

            OnNavigateToCustomersCommand = new DelegateCommand(() => NavigationService.Navigate("ClientView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewCustomer); });

            OnNavigateToAppointmentsCommand = new DelegateCommand(() => NavigationService.Navigate("VisitHistoryView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewAppointment); });

            OnNavigateToFinancialTransactionsCommand = new DelegateCommand(() => NavigationService.Navigate("FinancialView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewFinancialTransaction); });

            OnNavigateToAdministrationCommand = new DelegateCommand(() => NavigationService.Navigate("AdministrationView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewUser); });

            OnNavigateToStaffCommand = new DelegateCommand(() => NavigationService.Navigate("StaffView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.VIewStaff); });

            OnNavigateToDiscountsCommand = new DelegateCommand(() => NavigationService.Navigate("DiscountView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewConfiguration); });

            OnNavigateToServicesCommand = new DelegateCommand(() => NavigationService.Navigate("ServiceView", null, this),
                () => { return CurrentUser != null && CurrentUser.HasPrivilege(Privilege.ViewService); });

            //TODO 
            OnNavigateToQuestionnaireCommand = new DelegateCommand(() => NavigationService.Navigate("QuestionnaireReportView", null, this));


        }
    }
}
