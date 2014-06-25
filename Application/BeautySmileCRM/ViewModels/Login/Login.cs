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
using SmartClasses.Utils.DBUpdater;
using System.IO;
using System.Threading;

namespace BeautySmileCRM.ViewModels
{
    public class Login : BaseNavigationViewModel
    {
        private const string SYSDBA = "sysdba";
        private const string LOCALHOST = "localhost";
        private const string LOCALHOST_IP = "127.0.0.1";

        private string _account;
        private ObservableCollection<string> _usedAccounts;
        private string _password;
        private LoginStage _stage;
        private string _errorMessage;
        private string _server;
        private string _focuseTo;
        private bool _allowEditServer;
        private string _updatingMessage;
        public bool AllowEditServer
        {
            get { return _allowEditServer; }
            set
            {
                if(_allowEditServer != value)
                {
                    _allowEditServer = value;
                    RaisePropertyChanged("AllowEditServer");
                }
            }
        }

        public string Account 
        { 
            get { return _account; } 
            set
            {
                if(_account != value)
                {
                    _account = value;
                    RaisePropertyChanged("Account");
                    if(Account == SYSDBA)
                    {
                        Server = LOCALHOST;
                        AllowEditServer = false;
                        FocuseTo = "Password";
                    }
                    else
                    {
                        AllowEditServer = true;
                    }
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
        public LoginStage Stage
        {
            get { return _stage; }
            set
            {
                if (_stage != value)
                {
                    _stage = value;
                    RaisePropertiesChanged("Stage");
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
        public string UpdatingMessage
        {
            get { return _updatingMessage; }
            set
            {
                if(_updatingMessage != value)
                {
                    _updatingMessage = value;
                    RaisePropertyChanged("UpdatingMessage");
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
            _allowEditServer = true;
            Stage = LoginStage.Pending;
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
                Task.Factory.StartNew(() =>
                {
                    using(var dc = new CRMContext())
                    {
                        if (!dc.Database.Exists() && Account.ToLower() != SYSDBA)
                        {
                            ErrorMessage = String.Format("Ошибка подключения к базе данных: на сервере \"{0}\" база данных CRM не найдена!", Server);
                            Stage = Enums.LoginStage.Error;
                            return;
                        }
                        else if (Account.ToLower() == SYSDBA && Server.ToLower() != LOCALHOST && Server != LOCALHOST_IP)
                        {
                            Stage = LoginStage.Error;
                            ErrorMessage = "Запуск приложения от имени пользователя SYSDBA разрешено только с ПК-сервера базы данных!";
                            return;
                        } 
                        else if (Account.ToLower() == SYSDBA && (Server.ToLower() == LOCALHOST || Server == LOCALHOST_IP))
                        {
                            if(Password != "qwerty~123")
                            {
                                ErrorMessage = "Указан неверный пароль системного пользоваетеля";
                                Stage = Enums.LoginStage.Error;
                            }

                            var updater = new DBUpdater(BeautySmileCRM.Properties.Settings.Default.ServiceConnectionString, "CRM", "SERVICE", Resources.Packs.ResourceManager);
                            if (updater.IsNewVersionFound)
                            {
                                try
                                {
                                    Stage = LoginStage.Updating;
                                    UpdatingMessage = "Выполняется обновление БД...";
                                    Thread.Sleep(2000);

                                    updater.InstallUpdates();
                                    UpdatingMessage = String.Format("База данных успешно обновлена до версии {0}", updater.NewVersion);
                                    using (var outfile = new StreamWriter(String.Format("update_{0}.log", DateTime.Now.Date.ToString("ddMMyyyHHmmss"))))
                                    {
                                        outfile.Write(updater.Log.ToString());
                                    }
                                    Thread.Sleep(4000);
                                }
                                catch (Exception ex)
                                {
                                    Stage = LoginStage.Error;
                                    ErrorMessage = "Во время обновления произошла ошибка. Подробнее см. лог-файл обновления в корневой папке приложения.";
                                    using (var outfile = new StreamWriter(String.Format("update_{0}.log", DateTime.Now.Date.ToString("ddMMyyyHHmmss"))))
                                    {
                                        outfile.WriteLine(updater.Log.ToString());
                                        outfile.WriteLine(ex.Message);
                                    }
                                    return;
                                }
                            };
                        }
                        try
                        {
                            var dbVersionRow = dc.DBVersions
                                .OrderByDescending(x => x.Major)
                                .ThenByDescending(x => x.Minor)
                                .ThenByDescending(x => x.Build)
                                .ThenByDescending(x => x.Revision)
                                .FirstOrDefault();

                            if (dbVersionRow == null)
                            {
                                Stage = LoginStage.Error;
                                ErrorMessage = "База данных не иницализирована. Обратитесь к администратору для инициализации БД.";
                                return;
                            }

                            var dbVersion = new Version(dbVersionRow.Major, dbVersionRow.Minor, dbVersionRow.Build, dbVersionRow.Revision);
                            if (dbVersion != ApplicationService.AppVersion)
                            {
                                Stage = LoginStage.Error;
                                ErrorMessage = String.Format("Не совпадают версии БД ({0}) и приложения ({1})", dbVersion.ToString(), ApplicationService.AppVersion.ToString());
                                return;
                            }

                            Stage = Enums.LoginStage.Authorization;
                            var encriptedPassword = Password.ToMD5Hash();
                            var user = dc.Users
                                .Where(x => x.Login == Account && x.Password == encriptedPassword)
                                .Include(x => x.Privileges)
                                .SingleOrDefault();
                            if (user != null)
                            {
                                user.Password = "*".PadLeft(user.Password.Length, '*');

                                if (user.ExpirationDate.HasValue && user.ExpirationDate < DateTime.Now)
                                {
                                    ErrorMessage = String.Format("Учетная запись заблокирована с {0:d}", user.ExpirationDate);
                                    Stage = Enums.LoginStage.Error;
                                }
                                else if (!user.Privileges.Any(x => x.ID == (int)Enums.Privilege.Login))
                                {
                                    ErrorMessage = "Пользователь не имеет достаточно прав для запуска приложения";
                                    Stage = Enums.LoginStage.Error;
                                }
                                else
                                {
                                    CurrentUser = user;
                                    Stage = Enums.LoginStage.Authorized;
                                    NavigationService.Navigate("DashboardView", null, this);
                                    UserProfileService.SetLogins(user.Login);
                                    UserProfileService.SetServers(Server);
                                };
                            }
                            else
                            {
                                ErrorMessage = "Пользователь с указанным логином и паролем не найден";
                                Stage = Enums.LoginStage.Error;
                            }
                        }
                        catch (Exception e)
                        {
                            ErrorMessage = String.Format("Ошибка операции: {0}", e.Message);
                            Stage = Enums.LoginStage.Error;
                        }
                    };                    
                });   
            }
            else
            {
                ErrorMessage = "Не заполнен логин или пароль";
                Stage = Enums.LoginStage.Error;
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
