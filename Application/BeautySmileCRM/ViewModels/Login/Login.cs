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
using BeautySmileCRM.Services;

namespace BeautySmileCRM.ViewModels
{
    public class Login : BaseNavigationViewModel
    {
        private string _account;
        private ObservableCollection<string> _usedAccounts;
        private string _password;
        private AuthorizationStage _authorizationStage;
        private string _errorMessage;
        private string _server;
        private string _focuseTo;

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
        public string Server
        {
            get { return _server; }
            set
            {
                if(_server != value)
                {
                    _server = value;
                    RaisePropertyChanged("Server");
                }
            }
        }
        public string FocuseTo
        {
            get { return _focuseTo; }
            set
            {
                if(_focuseTo != value)
                {
                    _focuseTo = value;
                    RaisePropertyChanged("FocuseTo");
                }
            }
        }

        public IEnumerable<string> Logins
        {
            get { return UserProfileService.GetLogins(); }
        }
        public IEnumerable<string> Servers
        {
            get { return UserProfileService.GetServers(); }
        }

        public ICommand OnRetryLoginCommand { get; private set; }
        public ICommand OnLoginCommand { get; private set; }
        public ICommand OnPasswordKeyUpCommand { get; private set; }

        public Login()
        {
            initialize();
            OnRetryLoginCommand = new DelegateCommand(OnRetryLoginCommandExecute);
            OnLoginCommand = new DelegateCommand(OnLoginCommandExecute);
            OnPasswordKeyUpCommand = new DelegateCommand<KeyEventArgs>(OnPasswordKeyUpCommandExecuted);

            Account = Logins.FirstOrDefault();
            Server = Servers.FirstOrDefault();
#if(DEBUG)
            /*Password = "qwerty~123";
            Server = "localhost";*/
#endif
            SetFocusTo();
        }

        private void initialize()
        {
            AuthorizationStage = AuthorizationStage.Pending;
            ErrorMessage = String.Empty;
            Password = String.Empty;
        }

        private void OnRetryLoginCommandExecute()
        {
            initialize();
            SetFocusTo();
        }
        private void OnLoginCommandExecute()
        {
            if (!String.IsNullOrWhiteSpace(Account)
                && !String.IsNullOrWhiteSpace(Password)
                && !String.IsNullOrWhiteSpace(Server))
            {
                UserProfileService.Server = Server;
                AuthorizationStage = Enums.AuthorizationStage.Authorization;
                Task.Factory.StartNew(() =>
                {
                    using(var dc = new CRMContext())
                    {
                        if (dc.Database.Exists())
                        {

                            var encriptedPassword = Password.ToMD5Hash();
                            var user = dc.Users
                                .Where(x => x.Login == Account && x.Password == encriptedPassword)
                                .Include(x => x.Privileges)
                                .SingleOrDefault();
                            if (user != null)
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
                                    UserProfileService.SetLogins(user.Login);
                                    UserProfileService.SetServers(Server);
                                };
                            }
                            else
                            {
                                ErrorMessage = "Пользователь с указанным логином и паролем не найден";
                                AuthorizationStage = Enums.AuthorizationStage.Error;
                            }
                        }
                        else
                        {
                            ErrorMessage = String.Format("Ошибка подключения к базе данных: на сервере {0} база данных CRM не найдена!", Server);
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
        private void OnPasswordKeyUpCommandExecuted(KeyEventArgs args)
        {
            if(args.Key == Key.Enter)
            {
                OnLoginCommandExecute();
            }
        }

        private void SetFocusTo()
        {
            if (!String.IsNullOrWhiteSpace(Password))
            {
                FocuseTo = "Login";
            }
            else if (!String.IsNullOrWhiteSpace(Server))
            {
                FocuseTo = "Password";
            }
            else if (!String.IsNullOrWhiteSpace(Account))
            {
                FocuseTo = "Server";
            }
            else
            {
                FocuseTo = "Account";
            };
        }
    }
}
