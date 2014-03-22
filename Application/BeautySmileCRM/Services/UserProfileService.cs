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

        public static List<string> GetLogins()
        {
            var logins = Properties.Settings.Default.Logins;
            return (!String.IsNullOrWhiteSpace(logins)) ? logins.ToLower().Split(',').ToList() : new List<string>();
        }
        public static void SetLogins(string login)
        {
            var logins = GetLogins();
            if (logins.Any(x => x == login.ToLower()))
            {
                logins.Remove(login);
            };
            logins.Insert(0, login);
            Properties.Settings.Default.Logins = String.Join(",", logins);
            Properties.Settings.Default.Save();
        }
    }
}
