﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySmileCRM.ViewModels.Base;
using DevExpress.Xpf.Mvvm;
using DevExpress.Xpf.WindowsUI.Navigation;
using SmartClasses.Attributes;
using SmartClasses.Utils;
using SmartClasses.Extensions;
using DevExpress.Xpf.Core.ServerMode;
using System.Data.Entity;
using System.Windows.Input;
using System.Windows;
using BeautySmileCRM.Enums;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Microsoft.Win32;

namespace BeautySmileCRM.ViewModels
{
    public class ClientPage : BaseNavigationViewModel, IDataErrorInfo
    {
        #region Validation
        public string Error { get; private set; }

        public string this[string columnName]
        {
            get 
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "FirstName":
                        if (String.IsNullOrWhiteSpace(this.FirstName))
                        {
                            errorMessage = "Имя не может быть пустым!";
                        };
                        break;
                    case "LastName":
                        if (String.IsNullOrWhiteSpace(this.LastName))
                        {
                            errorMessage = "Фамилия не может быть пустой!";
                        };
                        break;
                    case "Email":
                        if (!String.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
                        {
                            errorMessage = "Указан некорректный адрес эл.почты!";
                        };
                        break;
                };
                if ((columnName == "FirstName"
                     || columnName == "LastName"
                     || columnName == "MiddleName")
                    && this._customer != null
                    && _nameChanged && _dc.Customers.Any(x => x.ID != this._customer.ID && x.FirstName == this.FirstName && x.LastName == this.LastName && x.MiddleName == this.MiddleName))
                {
                    errorMessage = "Клиент с таким ФИО уже существует!";
                };
                return errorMessage;
            }
        }

        private bool Validate()
        {
            var validationProperties = AttributeUtils.GetProperties<ValidateAttribute>(this.GetType());
            var sb = new StringBuilder();
            foreach (var itm in validationProperties)
            {
                sb.Append(this[itm.Name]);
            };
            Error = sb.ToString();
            return String.IsNullOrWhiteSpace(Error);
        }
        #endregion

        private Models.CRMContext _dc;
        private Models.Customer _customer;
        private Models.DiscountCard _discountCard;
        private IEnumerable<Models.CumulativeDiscount> _cumulativeDiscounts;
        
        private IEnumerable<Models.Appointment> _visitHistory;
        private Models.Appointment _seletedAppointment;
        
        private IEnumerable<Models.FinancialTransaction> _financialTransactions;
        private Models.FinancialTransaction _selectedFinancialTransaction;

        private DXTabItem _selectedTab;

        private bool _allowSave = false;
        private bool _nameChanged = false;
        private bool _discountCardEnabled = false;

        public ICommand LinkDiscountCardCommand { get; private set; }
        public ICommand UnlinkDiscountCardCommand { get; private set; }
        public ICommand AddVisitCommand { get; private set; }
        public ICommand AddPaymentCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand ExportCommand { get; private set; }
        public ICommand DeleteCustomerCommand { get; private set; }
        public ICommand OnRecalculateDiscountCommand { get; private set; }

        public bool AllowSave
        {
            get { return _allowSave; }
            set
            {
                if(_allowSave != value)
                {
                    _allowSave = value;
                    RaisePropertyChanged("AllowSave");
                }
            }
        }
        public bool DiscountCardEnabled
        {
            get { return _discountCardEnabled; }
            set
            {
                if (_discountCardEnabled != value)
                {
                    _discountCardEnabled = value;
                    RaisePropertyChanged("DiscountCardEnabled");
                }
            }
        }

        public IEnumerable<Models.CumulativeDiscount> CumulativeDiscounts
        {
            get
            {
                if(_cumulativeDiscounts == null)
                {
                    _cumulativeDiscounts = _dc.CumulativeDiscounts.ToList();
                };
                return _cumulativeDiscounts;
            }
        }
        public IDictionary<int, string> Genders
        {
            get { return EnumUtils.ToDescriptionDictionary<Enums.Gender>(); }
        }
        public IEnumerable<string> Countries
        {
            get 
            { 
                return _dc.Customers
                    .Where(x => x.Country != null)
                    .Select(x => x.Country)
                    .Distinct()
                    .ToList(); 
            }
        }
        public IEnumerable<int?> Zipes
        {
            get
            {
                return _dc.Customers
                    .Where(x => x.Zip.HasValue)
                    .Select(x => x.Zip)
                    .Distinct()
                    .ToList();
            }
        }
        public IEnumerable<string> Regions
        {
            get
            {
                return _dc.Customers
                    .Where(x => x.Region != null)
                    .Select(x => x.Region)
                    .Distinct()
                    .ToList();
            }
        }
        public IEnumerable<string> Cities
        {
            get
            {
                return _dc.Customers
                    .Where(x => x.City != null)
                    .Select(x => x.City)
                    .Distinct()
                    .ToList();
            }
        }
        public IEnumerable<string> DiscountCards
        {
            get
            {
                using(var dc = new Models.CRMContext())
                {
                    return dc.DiscountCards
                        .Select(x => x.Code)
                        .ToList();
                }
            }
        }
        public IDictionary<int, string> Staff
        {
            get
            {
                return _dc.Staffs.ToDictionary(x => x.ID, y => y.ShortName);
            }
        }
        public IDictionary<int, string> Users
        {
            get
            {
                return _dc.Users.ToDictionary(x => x.ID, y => y.Login);
            }
        }
        public IDictionary<int, string> AppointmentStates
        {
            get { return EnumUtils.ToDescriptionDictionary<Enums.AppointmentState>(); }
        }
        public IDictionary<int, string> TransactionTypes
        {
            get { return EnumUtils.ToDescriptionDictionary<Enums.TransactionType>(); }
        }

        public IEnumerable<Models.Appointment> VisitHistory 
        { 
            get { return _visitHistory; } 
            set
            {
                if(_visitHistory != value)
                {
                    _visitHistory = value;
                    RaisePropertyChanged("VisitHistory");
                }
            }
        }
        public Models.Appointment SelectedAppointment
        {
            get { return _seletedAppointment; }
            set
            {
                if (_seletedAppointment != value)
                {
                    _seletedAppointment = value;
                    RaisePropertyChanged("SelectedAppointment");
                };
            }
        }

        public IEnumerable<Models.FinancialTransaction> FinancialTransactions
        {
            get { return _financialTransactions; } 
            set
            {
                if(_financialTransactions != value)
                {
                    _financialTransactions = value;
                    RaisePropertyChanged("FinancialTransactions");
                }
            }
        }
        public Models.FinancialTransaction SelectedFinancialTransaction
        {
            get { return _selectedFinancialTransaction; }
            set
            {
                if (_selectedFinancialTransaction != value)
                {
                    _selectedFinancialTransaction = value;
                    RaisePropertyChanged("SelectedFinancialTransaction");
                }
            }
        }

        public DXTabItem SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if(_selectedTab != value)
                {
                    _selectedTab = value;
                    RaisePropertyChanged("SelectedTab");

                    var properties = BindGroupAttribute.GetPropertiesOfGroup(this.GetType(), "Commands");
                    RaisePropertiesChanged(properties.Select(x => x.Name).ToArray());
                }
            }
        }

        [Validate(), BindGroup("Customer")]
        public string FirstName 
        {
            get { return _customer != null ? _customer.FirstName : String.Empty; } 
            set
            {
                if(_customer != null && _customer.FirstName != value)
                {
                    _customer.FirstName = value;
                    RaisePropertyChanged("FirstName");
                    AllowSave = true;
                    _nameChanged = true;
                }
            }
        }
        [Validate(), BindGroup("Customer")]
        public string LastName
        {
            get { return _customer != null ? _customer.LastName : String.Empty; } 
            set
            {
                if(_customer != null && _customer.LastName != value)
                {
                    _customer.LastName = value;
                    RaisePropertyChanged("LastName");
                    AllowSave = true;
                    _nameChanged = true;
                }
            }
        }
        [BindGroup("Customer")]
        public string MiddleName
        {
            get { return _customer != null ? _customer.MiddleName : String.Empty; } 
            set
            {
                if(_customer != null && _customer.MiddleName != value)
                {
                    _customer.MiddleName = value;
                    RaisePropertyChanged("MiddleName");
                    AllowSave = true;
                    _nameChanged = true;
                }
            }
        }
        [BindGroup("Customer")]
        public int Gender
        {
            get { return _customer != null ? _customer.Gender : (int)Enums.Gender.Male; } 
            set
            {
                if(_customer != null && _customer.Gender != value)
                {
                    _customer.Gender = value;
                    RaisePropertyChanged("Gender");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public DateTime? BirthDate
        {
            get { return _customer != null ? _customer.BirthDate : null; } 
            set
            {
                if(_customer != null && _customer.BirthDate != value)
                {
                    _customer.BirthDate = value;
                    RaisePropertyChanged("BirthDate");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public decimal MoneyBalance
        {
            get { return _customer != null ? _customer.MoneyBalance : 0m; } 
        }
        [BindGroup("Customer")]
        public string Phone
        {
            get { return _customer != null ? _customer.Phone : String.Empty; } 
            set
            {
                if(_customer != null && _customer.Phone != value)
                {
                    _customer.Phone = value;
                    RaisePropertyChanged("Phone");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public string MobilePhone
        {
            get { return _customer != null ? _customer.MobilePhone : String.Empty; } 
            set
            {
                if(_customer != null && _customer.MobilePhone != value)
                {
                    _customer.MobilePhone = value;
                    RaisePropertyChanged("MobilePhone");
                    AllowSave = true;
                }
            }
        }
        [Validate(), BindGroup("Customer")]
        public string Email
        {
            get { return _customer != null ? _customer.Email : String.Empty; } 
            set
            {
                if(_customer != null && _customer.Email != value)
                {
                    _customer.Email = value;
                    RaisePropertyChanged("Email");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public string Country
        {
            get { return _customer != null ? _customer.Country : String.Empty; } 
            set
            {
                if(_customer != null && _customer.Country != value)
                {
                    _customer.Country = value;
                    RaisePropertyChanged("Country");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public int? Zip 
        {
            get { return _customer != null ? _customer.Zip : null; } 
            set
            {
                if(_customer != null && _customer.Zip != value)
                {
                    _customer.Zip = value;
                    RaisePropertyChanged("Zip");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public string Region
        {
            get { return _customer != null ? _customer.Region : String.Empty; } 
            set
            {
                if(_customer != null && _customer.Region != value)
                {
                    _customer.Region = value;
                    RaisePropertyChanged("Region");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public string City
        {
            get { return _customer != null ? _customer.City : String.Empty; } 
            set
            {
                if(_customer != null && _customer.City != value)
                {
                    _customer.City = value;
                    RaisePropertyChanged("City");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public string Address
        {
            get { return _customer != null ? _customer.Address : String.Empty; } 
            set
            {
                if(_customer != null && _customer.Address != value)
                {
                    _customer.Address = value;
                    RaisePropertyChanged("Address");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public bool NotifyByEmail 
        {
            get { return _customer != null ? _customer.NotifyByEmail : false; } 
            set
            {
                if(_customer != null && _customer.NotifyByEmail != value)
                {
                    _customer.NotifyByEmail = value;
                    RaisePropertyChanged("NotifyByEmail");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public bool NotifyBySms
        {
            get { return _customer != null ? _customer.NotifyBySms : false; } 
            set
            {
                if(_customer != null && _customer.NotifyBySms != value)
                {
                    _customer.NotifyBySms = value;
                    RaisePropertyChanged("NotifyBySms");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("Customer")]
        public bool NotifyByPost
        {
            get { return _customer != null ? _customer.NotifyByPost : false; } 
            set
            {
                if(_customer != null && _customer.NotifyByPost != value)
                {
                    _customer.NotifyByPost = value;
                    RaisePropertyChanged("NotifyByPost");
                    AllowSave = true;
                }
            }
        }

        [BindGroup("DiscountCard")]
        public string DiscountCardNumber
        {
            get { return _discountCard != null ? _discountCard.Code : String.Empty; } 
            set
            {
                if(_discountCard != null && _discountCard.Code != value)
                {
                    var card = _dc.DiscountCards.SingleOrDefault(x => x.Code == value);
                    if (card != null)
                    {
                        _customer.DiscountCard = card;
                        _discountCard = card;
                        RaisePropertiesChanged("NamedDiscount", "MinDiscount", "MaxDiscount");
                    }
                    else
                    {
                        _customer.DiscountCard = _discountCard;
                        _discountCard.Code = value;
                    }

                    RaisePropertyChanged("DiscountCardNumber");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("DiscountCard")]
        public int? NamedDiscount
        {
            get { return _discountCard != null ? (int?)CumulativeDiscounts.First(x => x.Percent == _discountCard.DiscountPercent).ID : null; }
            set
            {
                if(_discountCard != null && value.HasValue)
                {
                    var discount = CumulativeDiscounts.First(x => x.ID == value);
                    _discountCard.DiscountPercent = discount.Percent;
                    _discountCard.InitialDiscountPercent = discount.Percent;
                    RaisePropertyChanged("NamedDiscount");
                    MinDiscount = discount.MinDiscount;
                    MaxDiscount = discount.MaxDiscount;
                    AllowSave = true;
                }
            }
        }
        [BindGroup("DiscountCard")]
        public bool FixedDiscount
        {
            get { return _discountCard != null ? _discountCard.FixedDiscount : false; }
            set
            {
                if (_discountCard != null && _discountCard.FixedDiscount != value)
                {
                    _discountCard.FixedDiscount = value;
                    RaisePropertyChanged("FixedDiscount");
                    AllowSave = true;
                }
            }
        }
        [Validate(), BindGroup("DiscountCard")]
        public decimal? MinDiscount
        {
            get { return _discountCard != null ? (decimal?)_discountCard.MinDiscount : null; }
            set
            {
                if(_discountCard != null && value.HasValue)
                {
                    _discountCard.MinDiscount = value.Value;
                    RaisePropertyChanged("MinDiscount");
                    AllowSave = true;
                }
            }
        }
        [Validate(), BindGroup("DiscountCard")]
        public decimal? MaxDiscount
        {
            get { return _discountCard != null ? (decimal?)_discountCard.MaxDiscount : null; }
            set
            {
                if(_discountCard != null && value.HasValue)
                {
                    _discountCard.MaxDiscount = value.Value;
                    RaisePropertyChanged("MaxDiscount");
                    AllowSave = true;
                }
            }
        }
        [BindGroup("DiscountCard")]
        public decimal TotalPurchaseValue
        {
            get { return _discountCard != null ? _discountCard.TotalPurchaseValue : 0m; } 
        }

        protected IDialogService AppointmentDialogService { get { return GetService<IDialogService>("AppointmentEditDialog"); } }
        protected IDialogService FinancialTransactionDialogService { get { return GetService<IDialogService>("FinancialTransactionEditDialog"); } }
        protected IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }

        public string AddVisitCaption
        {
            get 
            {
                return "Зарегистрировать визит...";
            }
        }
        public string AddPaymentCaption
        {
            get
            {
                return "Зарегистрировать платеж...";
            }
        }
        [BindGroup("Commands")]
        public string EditCaption
        {
            get
            {
                return SelectedTab == null ? String.Empty : SelectedTab.Name == "visitHistoryTab" ? "Редактировать визит..." : "Изменить данные по оплате...";
            }
        }
        [BindGroup("Commands")]
        public string DeleteCaption
        {
            get
            {
                return SelectedTab == null ? String.Empty : SelectedTab.Name == "visitHistoryTab" ? "Удалить визит..." : "Удалить данные по оплате...";
            }
        }
        [BindGroup("Commands")]
        public string ExportCaption
        {
            get
            {
                return SelectedTab == null ? String.Empty : SelectedTab.Name == "visitHistoryTab" ? "Экспортировать в MS Excel данные по визитам" : "Экспортировать в MS Excel данные по финансовым операциям...";
            }
        }
        [BindGroup("Commands")]
        public bool AllowEditRow
        {
            get
            {
                if (SelectedTab == null)
                    return false;

                switch (SelectedTab.Name)
                {
                    case "visitHistoryTab":
                        return SelectedAppointment != null && CurrentUser.HasPrivilege(Privilege.ModifyAppointment);
                        
                    case "financialTransactionsTab":
                        if (SelectedFinancialTransaction == null)
                            return false;
                        return SelectedFinancialTransaction.TransactionTypeID != (int)TransactionType.Withdrawal
                            && CurrentUser.HasPrivilege(Privilege.ModifyFinancialTransaction);
                        
                };

                return false;
            }
        }
        [BindGroup("Commands")]
        public bool AllowAddVisit
        {
            get
            {
                return _customer != null && CurrentUser.HasPrivilege(Privilege.CreateAppointment);
            }
        }
        [BindGroup("Commands")]
        public bool AllowAddPayment
        {
            get
            {
                return _customer != null && CurrentUser.HasPrivilege(Privilege.CreateFinancialTransaction)
                    && SelectedAppointment != null
                    && (SelectedAppointment.StateID != (int)AppointmentState.Completed || SelectedAppointment.StateID != (int)AppointmentState.Canceled);
            }
        }
        [BindGroup("Commands")]
        public bool AllowDeleteRow
        {
            get
            {
                if (SelectedTab == null)
                    return false;

                switch (SelectedTab.Name)
                {
                    case "visitHistoryTab":
                        return SelectedAppointment != null && CurrentUser.HasPrivilege(Privilege.DeleteAppointment);

                    case "financialTransactionsTab":
                        if (SelectedFinancialTransaction == null)
                            return false;
                        return SelectedFinancialTransaction.TransactionTypeID != (int)TransactionType.Withdrawal
                            && CurrentUser.HasPrivilege(Privilege.DeleteFinancialTransaction);

                };

                return false;
            }
        }
        [BindGroup("Commands")]
        public bool ShowAddVisit
        {
            get
            {
                if (SelectedTab == null)
                    return false;

                return SelectedTab.Name == "visitHistoryTab";
            }
        }
        [BindGroup("Commands")]
        public bool ShowAddPayment
        {
            get
            {
                if (SelectedTab == null)
                    return false;

                return SelectedTab.Name == "visitHistoryTab";
            }
        }

        public bool AllowEditCustomer
        {
            get { return CurrentUser.HasPrivilege(Privilege.ModifyCustomer); }
        }
        public bool AllowEditDiscountCard
        {
            get { return CurrentUser.HasPrivilege(Privilege.ModifyDiscontCard); }
        }

        public bool ShowAppointmentsTab
        {
            get { return CurrentUser.HasPrivilege(Privilege.ViewAppointment); }
        }
        public bool ShowFinancialTransactionTab
        {
            get { return CurrentUser.HasPrivilege(Privilege.ViewFinancialTransaction); }
        }

        public ClientPage()
        {
            _dc = new Models.CRMContext();
            LinkDiscountCardCommand = new DelegateCommand(OnLinkDiscountCardCommandExecuted,
                () => { return !DiscountCardEnabled && CurrentUser.HasPrivilege(Privilege.LinkDiscountCard); });
            UnlinkDiscountCardCommand = new DelegateCommand(OnUnlinkDiscountCardCommandExecuted,
                () => { return DiscountCardEnabled && CurrentUser.HasPrivilege(Privilege.UnlinkDiscountCard); });

            AddVisitCommand = new DelegateCommand(OnAddAppointmentCommandExecuted,
                () => { return AllowAddVisit; });

            AddPaymentCommand = new DelegateCommand(OnAddPaymentCommandExecuted,
                () => { return AllowAddPayment; });

            EditCommand = new DelegateCommand(OnEditCommandExecuted,
                () => { return AllowEditRow; });
            DeleteCommand = new DelegateCommand(OnDeleteCommandExecuted,
                () => { return AllowDeleteRow; });
            ExportCommand = new DelegateCommand<object>(OnExportCommandExecuted,
                (x) => { return _customer != null; });

            DeleteCustomerCommand = new DelegateCommand(OnDeleteCustomerCommandExecuted,
                () => { return _customer != null && _customer.ID > 0 && CurrentUser.HasPrivilege(Privilege.DeleteCustomer); });

            OnRecalculateDiscountCommand = new DelegateCommand(OnRecalculateDiscountCommandExecuted,
                () => { return DiscountCardEnabled && CurrentUser.HasPrivilege(Privilege.LinkDiscountCard); });

            this.PropertyChanged += ClientPage_PropertyChanged;
        }

        private void ClientPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(AllowSave && Validate())
            {
                _dc.SaveChanges();
                AllowSave = false;
            }
        }
        
        protected override void OnNavigatedTo()
        {
            var customerID = (int?)this.Parameter;
            if (customerID.HasValue)
            {
                refreshData(customerID.Value);
            }
            else
            {
                defaultCustomer();
            };
        }

        private void OnLinkDiscountCardCommandExecuted()
        {
            var firstDiscount = _dc.CumulativeDiscounts
                .OrderBy(x => x.Percent)
                .FirstOrDefault();

            if (firstDiscount != null)
            {

                _discountCard = new Models.DiscountCard()
                {
                    Code = Models.DiscountCard.GenerateCode(),
                    DiscountPercent = firstDiscount.Percent,
                    MaxDiscount = firstDiscount.MaxDiscount,
                    MinDiscount = firstDiscount.MinDiscount,
                    DiscountTypeID = (int)Enums.DiscountType.Cumulative,
                    TotalPurchaseValue = 0
                };
                _customer.DiscountCard = _discountCard;
                DiscountCardEnabled = true;
                _dc.SaveChanges();
                RaisePropertiesChanged(BindGroupAttribute.GetPropertiesOfGroup(this.GetType(), "DiscountCard").Select(x => x.Name).ToArray());
            }
            else
            {
                MessageService.Show("Нельзя выполнить данную операцию, т.к. не заполнен справочник скидок!\nЗаполните справочник скидок и повторите попытку.", "Отсутствуют данные в справочнике", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }
        private void OnUnlinkDiscountCardCommandExecuted()
        {
            DiscountCardEnabled = false;
            var cardID = _discountCard.ID;
            var cardCode = _discountCard.Code;
            _discountCard = null;
            _customer.DiscountCard = null;
            
            RaisePropertiesChanged(BindGroupAttribute.GetPropertiesOfGroup(this.GetType(), "DiscountCard").Select(x => x.Name).ToArray());
            _dc.SaveChanges();

            if(!_dc.Customers.Any(x => x.DiscountCardID == cardID)
                && MessageService.Show(String.Format("Карта с кодом \"{0}\" не принадлежит ни одному клиенту, удалить карту из системы?", cardCode), "Подтвердите удаление картф", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var card = _dc.DiscountCards.Single(x => x.ID == cardID);
                _dc.DiscountCards.Remove(card);
                _dc.SaveChanges();
            }
        }

        private void OnAddAppointmentCommandExecuted()
        {
            var result = MessageBoxResult.None;
            result = ShowAppointmentEditDialog(DialogMode.Create);

            if (result == MessageBoxResult.Yes || result == MessageBoxResult.OK)
            {
                refreshData(_customer.ID);
            };


        }
        private void OnAddPaymentCommandExecuted()
        {
            var result = MessageBoxResult.None;
            result = ShowFinancialTransactionEditDialog(DialogMode.Create);

            if (result == MessageBoxResult.Yes || result == MessageBoxResult.OK)
            {
                refreshData(_customer.ID);
            };


        }

        private void OnEditCommandExecuted()
        {
            ShowDialog(DialogMode.Update);
        }
        private void OnDeleteCommandExecuted()
        {
            var result = false;
            switch (SelectedTab.Name)
            {
                case "visitHistoryTab":
                    if (MessageService.Show("Вы действительно хотите удалить выбранный визит, включая финансовые операции?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _dc.FinancialTransactions.RemoveRange(SelectedAppointment.FinancialTransactions);
                        _dc.AppointmentDetails.RemoveRange(SelectedAppointment.AppointmentDetails);
                        _dc.Appointments.Remove(SelectedAppointment);
                        result = true;
                    };
                    break;
                case "financialTransactionsTab":
                    if (MessageService.Show("Вы действительно хотите удалить выбранную финансовую операцию?", "Подтвердите удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        _dc.FinancialTransactions.Remove(SelectedFinancialTransaction);
                        result = true;
                    };
                    break;
            };

            if (result)
            {
                _dc.SaveChanges();
                refreshData(_customer.ID);
            }
        }
        private void OnExportCommandExecuted(object param)
        {
            var args = (object[])param;

            TableView table = null;

            var dlg = new SaveFileDialog()
            {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = "xlsx",
                Filter = "Файлы MS Excel 2007-2013|*.xlsx",
            };

            switch (SelectedTab.Name)
            {
                case "visitHistoryTab":
                    table = (TableView)args[0];
                    dlg.FileName = String.Format("Визиты {0}.xlsx", _customer.ShortName);
                    break;
                case "financialTransactionsTab":
                    table = (TableView)args[1];
                    dlg.FileName = String.Format("Финансовые операции {0}.xlsx", _customer.ShortName);
                    break;
            };

            if (dlg.ShowDialog() == true)
            {
                table.ExportToXlsx(dlg.FileName);
            };
        }
        private void OnDeleteCustomerCommandExecuted()
        {
            if(MessageService.Show(String.Format("Вы деействительно хотите удалить клиента \"{0}\"?", _customer.ShortName), "Подтвердите удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if(_customer.Appointments != null && _customer.Appointments.Count > 0)
                {
                    MessageService.Show("Нельзя удалить клиента, т.к. он имеет историю визитов.\nДля удаления клиента, необходимо удалить все его визиты и финансовые операции.", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (_customer.FinancialTransactions != null && _customer.FinancialTransactions.Count > 0)
                {
                    MessageService.Show("Нельзя удалить клиента, т.к. он имеет финансовые операции.\nДля удаления клиента, необходимо удалить все его визиты и финансовые операции.", "Ошибка удаления", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var cardID = _customer.DiscountCardID;
                    var customerID = _customer.ID;
                    _dc.Customers.Remove(_customer);
                    if (cardID.HasValue && !_dc.Customers.Any(x => x.DiscountCardID == cardID && x.ID != customerID))
                    {
                        _dc.DiscountCards.Remove(_dc.DiscountCards.Single(x => x.ID == cardID));
                    };
                    _dc.SaveChanges();
                    NavigationService.GoBack();
                };
            }
        }

        private void OnRecalculateDiscountCommandExecuted()
        {
            if(MessageService.Show(String.Format("Вы уверены, что хотите пересчитать скидку для дисконтной карты №{0}?", _discountCard.Code), "Подтвердите действие", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var recalcResult = _dc.RecalculateDiscount(_discountCard.ID);
                refreshData(_customer.ID);
            };
        }

        private void refreshData(int customerID)
        {
            _dc.Dispose();
            _dc = new Models.CRMContext();
            _customer = _dc.Customers
                    .Where(x => x.ID == customerID)
                    .Include(x => x.DiscountCard)
                    .Include(x => x.Appointments)
                    .Include(x => x.FinancialTransactions)
                    .First();

            _discountCard = _customer.DiscountCard;
            DiscountCardEnabled = _discountCard != null;

            VisitHistory = _customer.Appointments;
            FinancialTransactions = _customer.FinancialTransactions;

            refreshProperties();
        }

        private void defaultCustomer()
        {
            _customer = new Models.Customer()
            {
                Appointments = new List<Models.Appointment>(),
                FinancialTransactions = new List<Models.FinancialTransaction>()
            };

            _dc.Customers.Add(_customer);
            DiscountCardEnabled = false;

            VisitHistory = _customer.Appointments;
            FinancialTransactions = _customer.FinancialTransactions;

            refreshProperties();
        }
        private void refreshProperties()
        {
            var properties = AttributeUtils.GetProperties<BindGroupAttribute>(this.GetType());
            RaisePropertiesChanged(properties.Select(x => x.Name).ToArray());
        }

        private void ShowDialog(DialogMode mode)
        {
            var result = MessageBoxResult.None;
            switch(SelectedTab.Name)
            {
                case "visitHistoryTab":
                    result = ShowAppointmentEditDialog(mode);
                    break;
                case "financialTransactionsTab":
                    result = ShowFinancialTransactionEditDialog(mode);
                    break;
            };
            if (result == MessageBoxResult.Yes || result == MessageBoxResult.OK)
            {
                refreshData(_customer.ID);
            };
        }
        private MessageBoxResult ShowAppointmentEditDialog(DialogMode mode)
        {
            AppointmentEdit viewModel = null;
            switch (mode)
            {
                case DialogMode.Create:
                    viewModel = new AppointmentEdit(mode, _customer.ID, AppointmentDialogService, MessageService);
                    break;
                case DialogMode.Update:
                case DialogMode.View:
                    viewModel = new AppointmentEdit(mode, SelectedAppointment.ID, _customer.ID, AppointmentDialogService, MessageService);
                    break;
            }
            return viewModel.ShowEditDialog();
        }
        private MessageBoxResult ShowFinancialTransactionEditDialog(DialogMode mode, bool allowChangeAppointment = true)
        {
            FinancialTransactionEdit viewModel = null;
            switch (mode)
            {
                case DialogMode.Create:
                    viewModel = new FinancialTransactionEdit(mode, _customer.ID, (SelectedAppointment != null) ? (int?)SelectedAppointment.ID : null, FinancialTransactionDialogService, MessageService)
                    {
                        AllowSelectVisit = false
                    };
                    break;
                case DialogMode.Update:
                case DialogMode.View:
                    viewModel = new FinancialTransactionEdit(mode, SelectedFinancialTransaction.ID, _customer.ID, (SelectedAppointment != null) ? (int?)SelectedAppointment.ID : null, FinancialTransactionDialogService, MessageService)
                    {
                        AllowSelectVisit = false
                    };
                    break;
            };
            

            return viewModel.ShowEditDialog();
        }

    }

}
