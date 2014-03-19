using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;
using BeautySmileCRM.ViewModels.Base;
using local = BeautySmileCRM.Services;

namespace BeautySmileCRM.ViewModels
{
    public class MainWindow : BaseNavigationViewModel
    {
        public ICommand OnViewLoadedCommand { get; private set; }
        public MainWindow()
        {
            OnViewLoadedCommand = new DelegateCommand(OnNavigateLoginCommandExecute);
        }
        private void OnNavigateLoginCommandExecute() 
        {
            local.NavigationService.Service = ServiceContainer.GetService<INavigationService>();
            NavigationService.Navigate("LoginView", null, this);
        }
    }
}
