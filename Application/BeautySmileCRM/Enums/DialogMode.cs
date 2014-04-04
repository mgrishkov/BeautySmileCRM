using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Enums
{
    public enum DialogMode
    {
        [Description("Просмотр")]
        View = 0,
        [Description("Создание")]
        Create = 1,
        [Description("Редактирование")]
        Update = 2,
        [Description("Удаление")]
        Delete = 3
    }
}
