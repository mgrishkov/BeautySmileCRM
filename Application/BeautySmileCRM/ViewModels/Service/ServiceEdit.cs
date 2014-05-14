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
    public class ServiceEdit : BaseEditDialogViewModel, IDataErrorInfo
    {
        #region Validation
        public override string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "Description":
                        if (String.IsNullOrWhiteSpace(this.Description))
                        {
                            errorMessage = "Описание услуги не может быть пустым!";
                        } else if(_descriptionChanged && _dc.Services.Any(x => x.Description == this.Description))
                        {
                            errorMessage = "Услуга с таким описанием уже существует!";
                        }
                        break;
                };
                return errorMessage;
            }
        }
        #endregion
        private bool _descriptionChanged = false;
        private Models.Service _data;

        [Validate()]
        public string Description 
        {
            get { return _data.Description; }
            set
            {
                if (_data.Description != value)
                {
                    _data.Description = value;
                    RaisePropertyChanged("Description");
                    AllowSave = true;
                    _descriptionChanged = true;
                }
            }
        }
        public int WorkingMinutes
        {
            get { return _data.WorkingMinuts; }
            set
            {
                if(_data.WorkingMinuts != value)
                {
                    _data.WorkingMinuts = value;
                    RaisePropertyChanged("WorkingMinutes");
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
                }
            }
        }

        public ServiceEdit(DialogMode mode, int? serviceID, IDialogService dialogService, IMessageBoxService messageService)
            : base(mode, dialogService, messageService)
        {
            SubTitle = "Услугу";
            if (serviceID.HasValue)
            {
                _data = _dc.Services.SingleOrDefault(x => x.ID == serviceID);
            }
            else
            {
                _data = new Models.Service();
                _dc.Services.Add(_data);
            }
        }
        public ServiceEdit(DialogMode mode, IDialogService dialogService, IMessageBoxService messageService)
            : this(mode, (int?)null, dialogService, messageService)
        {

        }

        protected override void ApplyCommandExecuted()
        {
            _dc.SaveChanges();
        } 
    }
}
