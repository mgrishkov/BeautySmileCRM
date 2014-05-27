using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Services
{
    public class SesionService
    {
        public static IDictionary<string, object> Cache { get; private set; }

        static SesionService()
        {
            Cache = new Dictionary<string, object>();
        }        

        public static void ClearCache()
        {
            Cache.Clear();
        }
    }
}
