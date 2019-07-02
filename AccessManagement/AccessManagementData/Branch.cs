using System;
using System.Collections.Generic;

namespace AccessManagementData
{
    [Serializable]
    public partial class Branch
    {
        public Branch()
        {
            Account = new HashSet<Account>();
            InverseParentBranch = new HashSet<Branch>();
        }

        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int? ParentBranchId { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string City { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateUserId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Branch ParentBranch { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Branch> InverseParentBranch { get; set; }
    }
}
