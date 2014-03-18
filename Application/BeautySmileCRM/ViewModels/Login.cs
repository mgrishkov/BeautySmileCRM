using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using BeautySmileCRM.Enums;
using BeautySmileCRM.Models;
using DevExpress.Xpf.Mvvm;
using SmartClasses.Extensions;
using System.Data.Entity;
using BeautySmileCRM.ViewModels.Base;

namespace BeautySmileCRM.ViewModels
{
    public class Login : BaseNavigationViewModel
    {
        private string _account;
        private ObservableCollection<string> _usedAccounts;
        private string _password;
        private AuthorizationStage _authorizationStage;
        private string _errorMessage;        

        public string Account 
        { 
            get { return _account; } 
            set
            {
                if(_account != value)
                {
                    _account = value;
                    RaisePropertyChanged("Account");
                }
            }
        }
        public ObservableCollection<string> UsedAccounts
        {
            get { return _usedAccounts; }
            set
            {
                if (_usedAccounts != value)
                {
                    _usedAccounts = value;
                    RaisePropertyChanged("UsedAccounts");
                }
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }
        public AuthorizationStage AuthorizationStage
        {
            get { return _authorizationStage; }
            set
            {
                if (_authorizationStage != value)
                {
                    _authorizationStage = value;
                    RaisePropertiesChanged("AuthorizationStage");
                }
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                if(_errorMessage != value)
                {
                    _errorMessage = value;
                    RaisePropertyChanged("ErrorMessage");
                }
            }
        }

        public ICommand RetryLoginCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }

        public Login()
        {
            initialize();
            RetryLoginCommand = new DelegateCommand(onRetryLoginCommandExecute);
            LoginCommand = new DelegateCommand(onLoginCommandExecute);
#if(DEBUG)
            Account = "sysdba";
            Password = "qwerty~123";
#endif
        }

        private void initialize()
        {
            AuthorizationStage = AuthorizationStage.Pending;
            ErrorMessage = String.Empty;
            Password = String.Empty;
        }

        private void onRetryLoginCommandExecute()
        {
            initialize();
        }
        private void onLoginCommandExecute()
        {
            if (!String.IsNullOrWhiteSpace(Account)
                && !String.IsNullOrWhiteSpace(Password))
            {
                AuthorizationStage = Enums.AuthorizationStage.Authorization;
                Task.Factory.StartNew(() =>
                {
                    using(var dc = new CRMContext())
                    {
                        var encriptedPassword = Password.ToMD5Hash();
                        var user = dc.Users
                            .Where(x => x.Login == Account && x.Password == encriptedPassword)
                            .Include(x => x.Privileges)
                            .SingleOrDefault();
                        if(user != null)
                        {
                            if (user.ExpirationDate.HasValue && user.ExpirationDate < DateTime.Now)
                            {
                                ErrorMessage = String.Format("Учетная запись заблокирована с {0:d}", user.ExpirationDate);
                                AuthorizationStage = Enums.AuthorizationStage.Error;
                            }
                            else if (!user.Privileges.Any(x => x.ID == (int)Enums.Privilege.Login))
                            {
                                ErrorMessage = "Пользователь не имеет достаточно прав для запуска приложения";
                                AuthorizationStage = Enums.AuthorizationStage.Error;
                            }
                            else
                            {
                                user.Password = "*".PadLeft(user.Password.Length, '*');
                                CurrentUser = user;
                                AuthorizationStage = Enums.AuthorizationStage.Authorized;
                                NavigationService.Navigate("DashboardView", null, this);
                            };
                        }
                        else
                        {
                            ErrorMessage = "Пользователь с указанным логином и паролем не найден";
                            AuthorizationStage = Enums.AuthorizationStage.Error;
                        }
                    };                    
                });   
            }
            else
            {
                ErrorMessage = "Не заполнен логин или пароль";
                AuthorizationStage = Enums.AuthorizationStage.Error;
            };
        }
    }
}
