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
    public class StaffEdit : BaseEditDialogViewModel, IDataErrorInfo
    {
        #region Validation
        public override string this[string columnName]
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
                        }
                        break;
                    case "LastName":
                        if (String.IsNullOrWhiteSpace(this.LastName))
                        {
                            errorMessage = "Фамилия не может быть пустым!";
                        }
                        break;
                    case "MiddleName":
                        if (String.IsNullOrWhiteSpace(this.LastName))
                        {
                            errorMessage = "Отчество не может быть пустым!";
                        }
                        break;
                    case "Position":
                        if(String.IsNullOrWhiteSpace(this.Position))
                        {
                            errorMessage = "Должность не может быть пустой!";
                        };
                        break;
                };
                if(String.IsNullOrWhiteSpace(errorMessage)
                   && _nameChanged
                   && _dc.Staffs.Any(x => x.FirstName == this.FirstName && x.LastName == this.LastName && x.MiddleName == this.MiddleName))
                {
                    errorMessage = "Сотрудник с таким ФИО уже существует!";
                };
                return errorMessage;
            }
        }
        #endregion

        private Models.Staff _data;
        private bool _servicesWereChanged = false;
        private List<object> _staffServices;

        public IEnumerable<Models.Service> Services
        {
            get
            {
                return _dc.Services.OrderBy(x => x.Description).ToList();
            }
        }

        private bool _nameChanged = false;

        [Validate()]
        public string FirstName 
        {
            get { return _data.FirstName; }
            set
            {
                if (_data.FirstName != value)
                {
                    _data.FirstName = value;
                    RaisePropertyChanged("FirstName");
                    _nameChanged = true;
                    AllowSave = true;
                }
            }
        }
        [Validate()]
        public string LastName
        {
            get { return _data.LastName; }
            set
            {
                if (_data.LastName != value)
                {
                    _data.LastName = value;
                    RaisePropertyChanged("LastName");
                    _nameChanged = true;
                }
            }
        }
        [Validate()]
        public string MiddleName
        {
            get { return _data.MiddleName; }
            set
            {
                if (_data.MiddleName != value)
                {
                    _data.MiddleName = value;
                    RaisePropertyChanged("MiddleName");
                    _nameChanged = true;
                    AllowSave = true;
                }
            }
        }
        [Validate()]
        public string Position
        {
            get { return _data.Position; }
            set
            {
                if (_data.Position != value)
                {
                    _data.Position = value;
                    RaisePropertyChanged("Position");
                    AllowSave = true;
                }
            }
        }
        [Validate()]
        public DateTime? DismissalDate
        {
            get { return _data.DismissalDate; }
            set
            {
                if (_data.DismissalDate != value)
                {
                    _data.DismissalDate = value;
                    RaisePropertyChanged("DismissalDate");
                    AllowSave = true;
                }
            }
        }
        
        public List<object> StaffServices
        {
            get { return _staffServices; }
            set
            {
                if (_staffServices != value)
                {
                    _staffServices = value;
                    _servicesWereChanged = true;
                    AllowSave = true;
                    RaisePropertyChanged("StaffServices");
                }
            }
        }

        public IEnumerable<string> Positions
        {
            get 
            {
                return _dc.Staffs.Select(x => x.Position).Distinct().ToList();
            }
        }

        public StaffEdit(DialogMode mode, int? staffID, IDialogService dialogService, IMessageBoxService messageService)
            : base(mode, dialogService, messageService)
        {
            SubTitle = "Сотрудника";
            if (staffID.HasValue)
            {
                _data = _dc.Staffs.SingleOrDefault(x => x.ID == staffID);
            }
            else
            {
                _data = new Models.Staff();
                _dc.Staffs.Add(_data);
            };
            _staffServices = new List<object>(_data.Services.Cast<object>().ToList());
        }
        public StaffEdit(DialogMode mode, IDialogService dialogService, IMessageBoxService messageService)
            : this(mode, (int?)null, dialogService, messageService)
        {

        }

        protected override void ApplyCommandExecuted()
        {
            if (_servicesWereChanged)
            {
                var list = StaffServices.Cast<Models.Service>();
                _data.Services.Replace(list);
            };
            _dc.SaveChanges();
        } 
    }
}
