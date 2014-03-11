using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DevExpress.Xpf.Mvvm;
using BeautySmileCRM.ViewModels.Base;

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
            NavigationService.Navigate("LoginView", null, this);
        }
    }
}
