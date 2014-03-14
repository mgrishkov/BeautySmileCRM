using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeautySmileCRM.Models;
using DevExpress.Xpf.Mvvm;

namespace BeautySmileCRM.ViewModels.Base
{
    public class BaseNavigationViewModel : NavigationViewModelBase
    {
        public User CurrentUser
        {
            get { return Services.UserProfileService.CurrentUser; }
            set 
            {
                Services.UserProfileService.CurrentUser = value;
            }
        }

        public INavigationService NavigationService
        {
            get { return ServiceContainer.GetService<INavigationService>(); }
        }

        public BaseNavigationViewModel()
        {
            
        }
    }
}
