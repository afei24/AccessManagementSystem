using System;
using System.Collections.Generic;

namespace AccessManagementData
{
    public partial class Role
    {
        public Role()
        {
            AccountRole = new HashSet<AccountRole>();
            FunctionRole = new HashSet<FunctionRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<AccountRole> AccountRole { get; set; }
        public virtual ICollection<FunctionRole> FunctionRole { get; set; }
    }
}
