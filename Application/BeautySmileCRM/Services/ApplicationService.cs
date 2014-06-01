using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Services
{
    public class ApplicationService
    {
        public const string SufficientAuthority = "У вас недостаточно полномочий для выполнения этой операции";

        public static Version AppVersion
        {
            get
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                return assembly.GetName().Version;
            }
        }
    }
}
