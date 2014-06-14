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
    public class FinancialTransactionEdit : BaseEditDialogViewModel, IDataErrorInfo
    {
        #region Validation
        public override string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "Amount":
                        if (Amount <= 0)
                        {
                            errorMessage = "Сумма должна быть больше нуля!";
                        }
                        break;
                };
                return errorMessage;
            }
        }
        #endregion

        private Models.FinancialTransaction _data;
        private bool _allowSelectVisit = true;

        public bool AllowSelectVisit
        {
            get { return _allowSelectVisit; }
            set
            {
                if(_allowSelectVisit != value)
                {
                    _allowSelectVisit = value;
                    RaisePropertyChanged("AllowSelectVisit");
                }
            }
        }

        public string ClientFullName 
        {
            get { return _data.Customer.FullName; }
        }
        public string DisountCardCode
        {
            get { return _data.Customer.DiscountCard != null ? _data.Customer.DiscountCard.Code : String.Empty; }
        }
        
        public int AppointmentID
        {
            get { return (_data.Appointment != null) ? _data.Appointment.ID : -1; }
            set
            {
                if (_data.Appointment != null && _data.Appointment.ID != value)
                {
                    _data.Appointment = _data.Customer.Appointments.Single(x => x.ID == value);
                    
                    RaisePropertyChanged("AppointmentID");
                    RaisePropertyChanged("Amount");
                    RaisePropertyChanged("ToPay");
                    RaisePropertyChanged("Payed");
                    RaisePropertyChanged("Residue");
                    AllowSave = true;
                }
            }
        }

        public decimal ToPay
        {
            get { return _data.Appointment != null ? _data.Appointment.ToPay : 0m; }
        }
        public decimal Payed
        {
            get 
            { 
                return _data.Appointment
                    .FinancialTransactions
                    .Where(x => x.TransactionTypeID == (int)TransactionType.Deposit)
                    .Sum(x => x.Amount); 
            }
        }
        public decimal Residue
        {
            get 
            { 
                return ToPay - Payed; 
            }
        }
        [Validate()]
        public decimal Amount
        {
            get { return _data.Amount; }
            set
            {
                if(_data.Amount != value)
                {
                    _data.Amount = value;
                    RaisePropertyChanged("Amount");
                    AllowSave = true;
                }
            }
        }
        public string Comment
        {
            get { return _data.Comment; }
            set
            {
                if(_data.Comment != value)
                {
                    _data.Comment = value;
                    RaisePropertyChanged("Comment");
                    AllowSave = true;
                }
            }
        }

        public IDictionary<int, string> CustomerVisits
        {
            get
            {
                return _data.Customer.Appointments
                    .Where(x => x.StateID != (int)AppointmentState.Canceled || x.StateID != (int)AppointmentState.Completed)
                    .OrderByDescending(x => x.StartTime)
                    .ToDictionary(x => x.ID, y => y.Title); 
            }
        }

        public FinancialTransactionEdit(DialogMode mode, int? transactionID, int clientID, int? appointmentID, IDialogService dialogService, IMessageBoxService messageService)
            : base(mode, dialogService, messageService)
        {
            SubTitle = "финансовой операции";
            if (transactionID.HasValue)
            {
                _data = _dc.FinancialTransactions.SingleOrDefault(x => x.ID == transactionID);
              //  _data.ModifiedBy = CurrentUser.ID;
                _data.ModificationTime = DateTime.Now;
            }
            else
            {
                var client = _dc.Customers.Single(x => x.ID == clientID);
                Models.Appointment appointment = null;
                if(appointmentID.HasValue)
                    appointment = _dc.Appointments.SingleOrDefault(x => x.ID == appointmentID);
                
                _data = new Models.FinancialTransaction()
                {
                    Customer = client,
                    Appointment = appointment,
                    TransactionTypeID = (int)TransactionType.Deposit,
                    CreatedBy = CurrentUser.ID,
                    CreationTime = DateTime.Now,
                    Amount = 0m
                };
                _dc.FinancialTransactions.Add(_data);
            };
            
        }
        public FinancialTransactionEdit(DialogMode mode, int clientID, int? appointmentID, IDialogService dialogService, IMessageBoxService messageService)
            : this(mode, (int?)null, clientID, appointmentID, dialogService, messageService)
        {

        }

        protected override void ApplyCommandExecuted()
        {
            base.ApplyCommandExecuted();
            _dc.SaveChanges();

            if(Payed >= ToPay)
            {
                _data.Appointment.StateID = (int)AppointmentState.Completed;
                _dc.SaveChanges();
            };
                
        }
    }
}
