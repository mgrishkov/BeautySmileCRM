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

        public IDictionary<int, string> Staff
        {
            get
            {
                return _dc.Staffs.ToDictionary(x => x.ID, y => y.ShortName);
            }
        }

        public AppointmentEdit(DialogMode mode, int? appointmentID, int clientID, IDialogService dialogService, IMessageBoxService messageService)
            : base(mode, dialogService, messageService)
        {
            SubTitle = "визита";
            if (appointmentID.HasValue)
            {
                _data = _dc.Appointments.SingleOrDefault(x => x.ID == appointmentID);
                _data.ModifiedBy = CurrentUser.ID;
                _data.ModificationTime = DateTime.Now;
            }
            else
            {
                var client = _dc.Customers
                    .Where(x => x.ID == clientID)
                    .Include(x => x.DiscountCard)
                    .First();

                _data = new Models.Appointment()
                {
                    Customer = client,
                    StartTime = DateTime.Now.AddHours(1).StartOfHour(),
                    EndTime = DateTime.Now.AddHours(1).StartOfHour().AddHours(1),
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
            
        }
        public AppointmentEdit(DialogMode mode, int clientID, IDialogService dialogService, IMessageBoxService messageService)
            : this(mode, (int?)null, clientID, dialogService, messageService)
        {

        }

        private void recalcToPay()
        {
            Discount = Math.Round(Price * DiscountPercent, 2);
            ToPay = Price - Discount;
        }
    }
}
