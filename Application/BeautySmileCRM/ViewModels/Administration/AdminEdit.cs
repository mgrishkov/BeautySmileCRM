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
    public class AdminEdit : BaseEditDialogViewModel, IDataErrorInfo
    {
        #region Validation
        public override string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                switch (columnName)
                {
                    case "Account":
                        if (String.IsNullOrWhiteSpace(this.Account))
                        {
                            errorMessage = "Имя учетной записи не может быть пустым!";
                        }
                        else if (_accountChanged && _dc.Users.Any(x => x.Login == Account))
                        {
                            errorMessage = "Учетная запись с указанным именем уже сущестует!";
                        };
                        break;
                    case "Email":
                        if(!String.IsNullOrWhiteSpace(this.Email) && !this.Email.IsValidEmail())
                        {
                            errorMessage = "Неврный формат адреса электронной почты!";
                        };
                        break;
                    case "Password":
                    case "ConfirmPassword":
                        if (_passwordWasChanged)
                        {
                            if (String.IsNullOrWhiteSpace(this.Password))
                            {
                                errorMessage = "Пароль не может быть пустым!";
                            }
                            else if (String.IsNullOrWhiteSpace(this.ConfirmPassword))
                            {
                                errorMessage = "Введите пароль повторно для проверки корректности ввода!";
                            }
                            else if(this.Password != this.ConfirmPassword)
                            {
                                errorMessage = "Пароли не совпадают!";
                            };
                        };
                        break;
                    case "UserPrivileges":
                        if(this.UserPrivileges.Count == 0)
                        {
                            errorMessage = "Не выбраны полномочия пользователя!";
                        };
                        break;
                };
                return errorMessage;
            }
        }
        #endregion

        private Models.User _data;

        private bool _privilesWereChanged = false;
        private bool _passwordWasChanged = false;
        private bool _accountChanged = false;

        private string _password = "********";
        private string _confirmPassword = "********";
        private List<object> _userPrivileges;
        
        [Validate()]
        public string Account 
        {
            get { return _data.Login; }
            set
            {
                if (_data.Login != value)
                {
                    _data.Login = value;
                    RaisePropertyChanged("Account");
                    _accountChanged = true;
                    AllowSave = true;
                }
            }
        }
        [Validate()]
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    PasswordWasChanged = true;
                    RaisePropertyChanged("Password");
                    ConfirmPassword = String.Empty;
                }
            }
        }
        [Validate()]
        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                if (_confirmPassword != value)
                {
                    _confirmPassword = value;
                    RaisePropertyChanged("ConfirmPassword");
                    AllowSave = true;
                }
            }
        }
        [Validate()]
        public string Email
        {
            get { return _data.Email; }
            set
            {
                if (_data.Email != value)
                {
                    _data.Email = value;
                    RaisePropertyChanged("Email");
                    AllowSave = true;
                }
            }
        }
        [Validate()]
        public List<object> UserPrivileges
        {
            get { return _userPrivileges; }
            set
            {
                if(_userPrivileges != value)
                {
                    _userPrivileges = value;
                    _privilesWereChanged = true;
                    AllowSave = true;
                    RaisePropertyChanged("UserPrivileges");
                }
            }
        }

        public DateTime? ExpirationDate
        {
            get { return _data.ExpirationDate; }
            set
            {
                if(_data.ExpirationDate != value)
                {
                    _data.ExpirationDate = value;
                    RaisePropertyChanged("ExpirationDate");
                    AllowSave = true;
                }
            }
        }

        public bool PasswordWasChanged
        {
            get { return _passwordWasChanged; }
            set
            {
                if(_passwordWasChanged != value)
                {
                    _passwordWasChanged = value;
                    RaisePropertyChanged("PasswordWasChanged");
                }
            }
        }
        public bool AllowChangePrivilege
        {
            get
            {
                return CurrentUser.HasPrivilege(Privilege.GrantPrivelege);
            }
        }

        public IEnumerable<Models.Privilege> Privileges
        {
            get 
            {
                return _dc.Privileges.OrderBy(x => x.Description).ToList();
            }
        }

        public AdminEdit(DialogMode mode, int? userID, IDialogService dialogService, IMessageBoxService messageService)
            : base(mode, dialogService, messageService)
        {
            SubTitle = "Пользователя приложения";
            if(userID.HasValue)
            {
                _data = _dc.Users.SingleOrDefault(x => x.ID == userID);
            }
            else
            {
                _data = new Models.User();
                _dc.Users.Add(_data);
            };
            _userPrivileges = new List<object>(_data.Privileges.Cast<object>().ToList());
        }
        public AdminEdit(DialogMode mode, IDialogService dialogService, IMessageBoxService messageService)
            : this(mode, (int?)null, dialogService, messageService)
        {

        }

        protected override void ApplyCommandExecuted()
        {
            if(_passwordWasChanged)
            {
                _data.Password = Password.ToMD5Hash();
            };
            if(_privilesWereChanged)
            {
                var list = UserPrivileges.Cast<Models.Privilege>();
                _data.Privileges.Replace(list);
            };
            _dc.SaveChanges();
        } 

    }
}
