﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySmileCRM.Models
{
    public partial class User
    {
        public bool HasPrivilege(Enums.Privilege privilege)
        {
            return Privileges.Any(x => x.ID == (int)privilege);
        }
    }
}
