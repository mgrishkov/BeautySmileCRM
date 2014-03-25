using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartClasses.Extensions;

namespace BeautySmileCRM.Services
{
    public class UserProfileService
    {
        private static Models.User _currentUser;
        private static string _server;

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
        public static string Server 
        {
            get { return _server; }
            set
            {
                if(_server != value)
                {
                    _server = value;
                    BuidConnectionString();
                }
            }
        }
        public static string ConnectionString { get; private set;}
        
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

        public static List<string> GetServers()
        {
            var servers = Properties.Settings.Default.Servers;
            return (!String.IsNullOrWhiteSpace(servers)) ? servers.ToLower().Split(',').ToList() : new List<string>();
        }
        public static void SetServers(string server)
        {
            var servers = GetServers();
            if (servers.Any(x => x == server.ToLower()))
            {
                servers.Remove(server);
            };
            servers.Insert(0, server);
            Properties.Settings.Default.Servers = String.Join(",", servers);
            Properties.Settings.Default.Save();
        }

        private static void BuidConnectionString()
        {
            string connectionString = String.Format("Data Source={0};Initial Catalog=CRM;User ID=AppUser;Password=qwerty~123", Server);
 
            var scsb = new SqlConnectionStringBuilder(connectionString);
 
            var ecb = new EntityConnectionStringBuilder();
            ecb.Metadata = "res://*/Models.CRM.csdl|res://*/Models.CRM.ssdl|res://*/Models.CRM.msl";
            ecb.Provider = "System.Data.SqlClient";
            ecb.ProviderConnectionString = scsb.ConnectionString;

            ConnectionString = ecb.ConnectionString;

            /*var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-16\"?>");
            sb.AppendLine("<SerializableConnectionString xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">");
            sb.AppendFormatLine("<ConnectionString>Data Source={0};Initial Catalog=CRM;User ID=AppUser;Password=qwerty~123</ConnectionString>", Server);
            sb.AppendLine("<ProviderName>System.Data.SqlClient</ProviderName>");
            sb.AppendLine("</SerializableConnectionString>");
            ConnectionString = sb.ToString();*/
        }
    }
}
