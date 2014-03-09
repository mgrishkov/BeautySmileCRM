using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    public class CumulativeDiscountEdit : BaseEditDialogViewModel, IDataErrorInfo
    {
        #region Validation
        public override string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "Name":
                        if (String.IsNullOrWhiteSpace(this.Name))
                        {
                            errorMessage = "Название не может быть пустым!";
                        } else if(_nameChanged && _dc.CumulativeDiscounts.Any(x => x.Name == this.Name))
                        {
                            errorMessage = "Накопительная скидка с таким названием уже существует!";
                        }
                        break;
                };
                return errorMessage;
            }
        }
        #endregion

        private Models.CumulativeDiscount _data;

        private bool _nameChanged = false;

        [Validate()]
        public string Name 
        {
            get { return _data.Name; }
            set
            {
                if (_data.Name != value)
                {
                    _data.Name = value;
                    RaisePropertyChanged("Name");
                    _nameChanged = true;
                    AllowSave = true;
                }
            }
        }
        public decimal Percent
        {
            get { return _data.Percent; }
            set
            {
                if (_data.Percent != value)
                {
                    _data.Percent = value;
                    RaisePropertyChanged("Percent");
                    AllowSave = true;
                }
            }
        }
        public decimal MinDiscount
        {
            get { return _data.MinDiscount; }
            set
            {
                if (_data.MinDiscount != value)
                {
                    _data.MinDiscount = value;
                    RaisePropertyChanged("MinDiscount");
                    AllowSave = true;
                }
            }
        }
        public decimal MaxDiscount
        {
            get { return _data.MaxDiscount; }
            set
            {
                if (_data.MaxDiscount != value)
                {
                    _data.MaxDiscount = value;
                    RaisePropertyChanged("MaxDiscount");
                    AllowSave = true;
                }
            }
        }        
        public decimal PurchaseTopLimit
        {
            get { return _data.PurchaseTopLimit; }
            set
            {
                if (_data.PurchaseTopLimit != value)
                {
                    _data.PurchaseTopLimit = value;
                    RaisePropertyChanged("PurchaseTopLimit");
                    AllowSave = true;
                }
            }
        }

        public CumulativeDiscountEdit(DialogMode mode, int? staffID, IDialogService dialogService, IMessageBoxService messageService)
            : base(mode, dialogService, messageService)
        {
            SubTitle = "Накопительную скидку";
            if (staffID.HasValue)
            {
                _data = _dc.CumulativeDiscounts.SingleOrDefault(x => x.ID == staffID);
            }
            else
            {
                _data = new Models.CumulativeDiscount();
                _dc.CumulativeDiscounts.Add(_data);
            }
        }
        public CumulativeDiscountEdit(DialogMode mode, IDialogService dialogService, IMessageBoxService messageService)
            : this(mode, (int?)null, dialogService, messageService)
        {

        }

        protected override void ApplyCommandExecuted()
        {
            _dc.SaveChanges();
        } 
    }
}
