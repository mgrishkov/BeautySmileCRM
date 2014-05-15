using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.ViewModels
{
    public class ClientQuestionnaireViewModel
    {
        public DateTime Timestamp { get; private set; }

        public ClientQuestionnaireViewModel()
        {
            Timestamp = DateTime.Now;
        }
    }
}
