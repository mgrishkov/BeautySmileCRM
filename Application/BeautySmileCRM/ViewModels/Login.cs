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
using DevExpress.Xpf.Mvvm;

namespace BeautySmileCRM.ViewModels
{
    public class Login : NavigationViewModelBase
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
        public INavigationService NavigationService 
        { 
            get { return ServiceContainer.GetService<INavigationService>(); } 
        }

        public ICommand RetryLoginCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }

        public Login()
        {
            initialize();
            RetryLoginCommand = new DelegateCommand(onRetryLoginCommandExecute);
            LoginCommand = new DelegateCommand(onLoginCommandExecute);
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
                    System.Threading.Thread.Sleep(1000);
                    AuthorizationStage = Enums.AuthorizationStage.Authorized;

                    /*var profile = AdminProfile.FindProfile(Login, Password);
                    if (profile != null)
                    {
                        Profile.AdminProfile = profile;
                        this.Dispatcher.Invoke(() => AuthorizationStage = LoadingStage.Loaded);
                    }
                    else
                    {
                        ErrorMessage = "Администратор с указанным логином и паролем не найден";
                        this.Dispatcher.Invoke(() =>
                        {
                            AuthorizationStage = LoadingStage.Error;
                        });
                    };*/

                    NavigationService.Navigate("DashboardView", null, this);
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
