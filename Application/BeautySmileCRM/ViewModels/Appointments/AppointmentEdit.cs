using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using BeautySmileCRM.ViewModels.Base;
using DevExpress.Xpf.Mvvm;
using SmartClasses.Extensions;
using System.Collections.Specialized;
using BeautySmileCRM.Enums;
using System.ComponentModel;
using SmartClasses.Attributes;
using System.Windows;

namespace BeautySmileCRM.ViewModels
{
    public class AppointmentEdit : BaseEditDialogViewModel, IDataErrorInfo
    {
        #region Validation
        public override string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "StartTime":
                    case "EndTime":
                        if(StartTime > EndTime)
                        {
                            errorMessage = "Дата начала визита не может быть больше даты его окончания!";
                        };
                        break;
                    case "StaffID":
                        if (StaffID <= 0)
                        {
                            errorMessage = "Не указан лечащий врач!";
                        };
                        break;
                    case "Purpose":
                        if(String.IsNullOrWhiteSpace(Purpose))
                        {
                            errorMessage = "Не указана цель визита!";
                        }
                        break;
                };
                return errorMessage;
            }
        }
        #endregion

        private Models.Appointment _data;
        private DateTime _duration;

        public string ClientFullName 
        {
            get { return _data.Customer.FullName; }
        }
        public string DisountCardCode
        {
            get { return _data.Customer.DiscountCard != null ? _data.Customer.DiscountCard.Code : String.Empty; }
        }
        [Validate()]
        public DateTime StartTime
        {
            get { return _data.StartTime; }
            set
            {
                if (_data.StartTime != value)
                {
                    _data.StartTime = value;
                    RaisePropertyChanged("StartTime");
                    AllowSave = true;
                    EndTime = StartTime.AddHours(1);
                }
            }
        }
        [Validate()]
        public DateTime EndTime
        {
            get { return _data.EndTime; }
            set
            {
                if (_data.EndTime != value)
                {
                    _data.EndTime = value;
                    RaisePropertyChanged("EndTime");
                    AllowSave = true;
                }
            }
        }
        [Validate()]
        public DateTime Duration
        {
            get { return _duration; }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    RaisePropertyChanged("Duration");
                    AllowSave = true;
                    EndTime = StartTime.AddHours(_duration.Hour).AddMinutes(_duration.Minute);
                }
            }
        }
        [Validate()]
        public int StaffID
        {
            get { return _data.StaffID; }
            set
            {
                if (_data.StaffID != value)
                {
                    _data.StaffID = value;
                    RaisePropertyChanged("StaffID");
                    AllowSave = true;
                }
            }
        }
        [Validate()]
        public string Purpose
        {
            get { return _data.Purpose; }
            set
            {
                if (_data.Purpose != value)
                {
                    _data.Purpose = value;
                    RaisePropertyChanged("Purpose");
                    AllowSave = true;
                }
            }
        }

        public decimal Price
        {
            get { return _data.Price; }
            set
            {
                if (_data.Price != value)
                {
                    _data.Price = value;
                    RaisePropertyChanged("Price");
                    AllowSave = true;
                    recalcToPay();
                }
            }
        }
        public decimal DiscountPercent
        {
            get { return _data.DiscountPercent; }
            set
            {
                if (_data.DiscountPercent != value)
                {
                    _data.DiscountPercent = value;
                    RaisePropertyChanged("DiscountPercent");
                    AllowSave = true;
                    recalcToPay();
                }
            }
        }
        [Validate()]
        public decimal Discount
        {
            get { return _data.Discount; }
            set
            {
                if (_data.Discount != value)
                {
                    _data.Discount = value;
                    RaisePropertyChanged("Discount");
                }
            }
        }
        public decimal ToPay
        {
            get { return _data.ToPay; }
            set
            {
                if (_data.ToPay != value)
                {
                    _data.ToPay = value;
                    RaisePropertyChanged("ToPay");
                }
            }
        }

        public bool AllowQuickPay 
        {
            get { return Mode == DialogMode.Create && AllowSave && CurrentUser.HasPrivilege(Privilege.CreateFinancialTransaction); }
        }
        public bool AllowCancelVisit
        {
            get { return Mode == DialogMode.Update; }
        }

        public IDictionary<int, string> Staff
        {
            get
            {
                return _dc.Staffs.Where(x => !x.DismissalDate.HasValue || x.DismissalDate < _data.EndTime).ToDictionary(x => x.ID, y => String.Format("{0} - {1}", y.ShortName, y.Position));
            }
        }
        public IEnumerable<string> Purposes
        {
            get
            {
                return _dc.Appointments
                    .Select(x => x.Purpose)
                    .Distinct()
                    .Take(100)
                    .ToList();
            }
        }

        public AppointmentEdit(DialogMode mode, int? appointmentID, int clientID, IDialogService dialogService, IMessageBoxService messageService)
            : base(mode, dialogService, messageService)
        {
            SubTitle = "визита";
            if (appointmentID.HasValue)
            {
                _data = _dc.Appointments
                    .Where(x => x.ID == appointmentID)
                    .Include(x => x.Customer)
                    .Include(x => x.Customer.DiscountCard)
                    .First();
                _data.ModifiedBy = CurrentUser.ID;
                _data.ModificationTime = DateTime.Now;
            }
            else
            {
                var client = _dc.Customers
                    .Where(x => x.ID == clientID)
                    .Include(x => x.DiscountCard)
                    .First();

                var nextHour = DateTime.Now.AddHours(1).StartOfHour();
                _data = new Models.Appointment()
                {
                    Customer = client,
                    StartTime = nextHour,
                    EndTime = nextHour.AddHours(1),
                    CreatedBy = CurrentUser.ID,
                    CreationTime = DateTime.Now,
                    DiscountPercent = client.DiscountCard != null ? client.DiscountCard.DiscountPercent : 0m,
                    Price = 0m,
                    Discount = 0m,
                    ToPay = 0m,
                    StateID = (int)AppointmentState.Active
                };
                _dc.Appointments.Add(_data);
            };
            Duration = new DateTime(_data.StartTime.Year, _data.StartTime.Month, _data.StartTime.Day, (int)_data.EndTime.Subtract(_data.StartTime).TotalHours, 0, 0);
            
        }
        public AppointmentEdit(DialogMode mode, int clientID, IDialogService dialogService, IMessageBoxService messageService)
            : this(mode, (int?)null, clientID, dialogService, messageService)
        {

        }

        protected override void ExtendDialogCommands(List<UICommand> commands)
        {
            base.ExtendDialogCommands(commands);
            commands.Insert(0, new UICommand()
            {
                Id = MessageBoxResult.OK,
                Caption = "Оплатить",
                IsCancel = false,
                IsDefault = false,
                Command = new DelegateCommand<CancelEventArgs>(
                    OnDialogPayCommandtExecuting,
                    x => { return this.AllowQuickPay; }
                ),
            });
            commands.Insert(0, new UICommand()
            {
                Id = MessageBoxResult.OK,
                Caption = "Отменить визит",
                IsCancel = false,
                IsDefault = false,
                Command = new DelegateCommand(
                    OnDialogCancelCommandtExecuting,
                    ()=> { return this.AllowCancelVisit; }
                ),
            });
        }

        private void OnDialogPayCommandtExecuting(CancelEventArgs args)
        {
            if (Validate())
            {
                if (MessageService.Show(String.Format("Провести операцию оплаты за визит в размере {0:c}?", ToPay), "Регистрация оплаты визита", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var ft = new Models.FinancialTransaction()
                    {
                        Appointment = _data,
                        Customer = _data.Customer,
                        Amount = ToPay,
                        CreatedBy = Services.UserProfileService.CurrentUser.ID,
                        CreationTime = DateTime.Now,
                        TransactionTypeID = (int)TransactionType.Deposit
                    };
                    _data.StateID = (int)AppointmentState.Completed;
                    _dc.FinancialTransactions.Add(ft);
                    _dc.SaveChanges();
                };
            }
            else
            {
                args.Cancel = true;
            }
        }
        private void OnDialogCancelCommandtExecuting()
        {
            if (Validate())
            {
                if (MessageService.Show("Вы действительно хотите отменить визит?", "Отмена визита", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    _data.StateID = (int)AppointmentState.Canceled;
                    _dc.SaveChanges();
                };
            }
        }

        protected override void AllowSaveChanged()
        {
            base.AllowSaveChanged();
            RaisePropertyChanged("AllowQuickPay");
        }
        protected override void ApplyCommandExecuted()
        {
            base.ApplyCommandExecuted();
            if (Validate())
            {
                _dc.SaveChanges();
            };
        }

        private void recalcToPay()
        {
            var discount = Math.Round(Price * DiscountPercent, 2);

            if(discount < _data.Customer.DiscountCard.MinDiscount)
            {
                Discount = _data.Customer.DiscountCard.MinDiscount;
                DiscountPercent = Math.Round(Discount / Price, 2);
            }
            else if(discount > _data.Customer.DiscountCard.MaxDiscount)
            {
                discount = _data.Customer.DiscountCard.MaxDiscount;
                DiscountPercent = Math.Round(Discount / Price, 2);
            }
            else
            {
                Discount = discount;
            };

            if (Discount != discount)
                MessageService.Show(String.Format("Скидка была скорректирована, т.к расчитанный размер скидки [{0:c}] выходит за границы доступного размера скидки для данного клиента: [{1:c} - {2:c}].", discount, _data.Customer.DiscountCard.MinDiscount, _data.Customer.DiscountCard.MaxDiscount), "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);

            ToPay = Price - Discount;
        }
    }
}
