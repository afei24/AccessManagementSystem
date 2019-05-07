using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementData
{
    public class Branch
    {
        public Branch()
        {
            Branches = new HashSet<Branch>();
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

        public virtual Account CreateUser { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }

        public virtual Branch ParentBranch { get; set; }
    }
}
