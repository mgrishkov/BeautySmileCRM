using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Interfaces
{
    public interface IDialogUserControl
    {
        event EventHandler AllowSaveChanged;

        bool AllowSave { get; set; }
        bool Bind<T>(T ID);
        bool Save();
    }
}
