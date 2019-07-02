using System;
using System.Collections.Generic;

namespace AccessManagementData
{
    [Serializable]
    public partial class AccountFunction
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int FunctionId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Function Function { get; set; }
    }
}
