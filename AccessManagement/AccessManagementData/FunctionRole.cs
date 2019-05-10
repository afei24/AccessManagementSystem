﻿using System;
using System.Collections.Generic;

namespace AccessManagementData
{
    public partial class FunctionRole
    {
        public int Id { get; set; }
        public int FunctionId { get; set; }
        public int RoleId { get; set; }

        public virtual Function Function { get; set; }
        public virtual Role Role { get; set; }
    }
}
