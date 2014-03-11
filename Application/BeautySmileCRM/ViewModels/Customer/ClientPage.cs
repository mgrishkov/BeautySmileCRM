using System;
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
                }
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
            Error = String.Empty;
            return String.IsNullOrWhiteSpace(Error);
        }
        #endregion

        private readonly Models.CRMContext _dc;
        private Models.Customer _customer;
        private Models.DiscountCard _discountCard;
        private IEnumerable<Models.CumulativeDiscount> _cumulativeDiscounts;
        private IEnumerable<Models.Appointment> _visitHistory;

        private bool _allowSave = false;
        private bool _nameChanged = false;
        private bool _discountCardEnabled = false;

        public ICommand OnLinkDiscountCardCommand { get; private set; }
        public ICommand OnUnlinkDiscountCardCommand { get; private set; }

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
                return _dc.DiscountCards
                    .Select(x => x.Code)
                    .ToList();
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
                    _customer.Country = value;
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
                    _discountCard.Code = value;
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
                    _discountCard.DiscountPercent = CumulativeDiscounts.First(x => x.ID == value).Percent;
                    RaisePropertyChanged("NamedDiscount");
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

        protected IDialogService DialogService { get { return GetService<IDialogService>(); } }
        protected IMessageBoxService MessageService { get { return GetService<IMessageBoxService>(); } }

        public ClientPage()
        {
            _dc = new Models.CRMContext();
            OnLinkDiscountCardCommand = new DelegateCommand(OnLinkDiscountCardCommandExecuted);
            OnUnlinkDiscountCardCommand = new DelegateCommand(OnUnlinkDiscountCardCommandExecuted);
        }
        
        protected override void OnNavigatedTo()
        {
            var customerID = (int?)this.Parameter;
            if (customerID.HasValue)
            {
                _customer = _dc.Customers
                    .Where(x => x.ID == customerID)
                    .Include(x => x.DiscountCard)
                    .Include(x => x.Appointments)
                    .First();
                
                _discountCard = _customer.DiscountCard;
                DiscountCardEnabled = _discountCard != null;

                VisitHistory = _customer.Appointments;
            }
            else
            {
                _customer = new Models.Customer();
                VisitHistory = new List<Models.Appointment>();
                /*var initialDiscount = _dc.CumulativeDiscounts
                    .OrderBy(x => x.Percent)
                    .Take(1)
                    .First();

                var newCode = Models.DiscountCard.GenerateCode();

                _discountCard = new Models.DiscountCard()
                {
                    Code = newCode,
                    DiscountTypeID = (int)Enums.DiscountType.Cumulative,
                    DiscountPercent = initialDiscount.Percent,
                    MaxDiscount = initialDiscount.MaxDiscount,
                    MinDiscount = initialDiscount.MinDiscount
                };
                _customer.DiscountCard = _discountCard;
                 */
            };
            var properties = AttributeUtils.GetProperties<BindGroupAttribute>(this.GetType());
            RaisePropertiesChanged(properties.Select(x => x.Name).ToArray());
        }
        
        private void OnLinkDiscountCardCommandExecuted()
        {
            var firstDiscount = _dc.CumulativeDiscounts
                .OrderBy(x => x.Percent)
                .First();
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
            RaisePropertiesChanged(BindGroupAttribute.GetPropertiesOfGroup(this.GetType(), "DiscountCard").Select(x => x.Name).ToArray());
        }
        private void OnUnlinkDiscountCardCommandExecuted()
        {
            DiscountCardEnabled = false;
            _discountCard = null;
            RaisePropertiesChanged(BindGroupAttribute.GetPropertiesOfGroup(this.GetType(), "DiscountCard").Select(x => x.Name).ToArray());
        }
    }
}
