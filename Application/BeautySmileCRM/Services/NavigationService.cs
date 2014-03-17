using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Xpf.Mvvm;

namespace BeautySmileCRM.Services
{
    public class NavigationService
    {
        private static INavigationService _service;

        public static INavigationService Service
        {
            get { return _service; }
            set
            {
                if (_service == null)
                    _service = value;
            }
        }
    }
}
