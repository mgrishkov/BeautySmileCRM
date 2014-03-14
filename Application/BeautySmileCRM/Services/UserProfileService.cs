using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Services
{
    public class UserProfileService
    {
        private static Models.User _currentUser;

        public static Models.User CurrentUser 
        { 
            get { return _currentUser; }  
            set
            {
                if(_currentUser == null)
                {
                    _currentUser = value;
                };
            }
        }

    }
}
