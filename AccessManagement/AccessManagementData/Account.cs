using System;
using System.Collections.Generic;

namespace AccessManagementData
{
    public partial class Account
    {
        public Account()
        {
            AccountFunction = new HashSet<AccountFunction>();
            AccountRole = new HashSet<AccountRole>();
        }

        public int Id { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreateUserId { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<AccountFunction> AccountFunction { get; set; }
        public virtual ICollection<AccountRole> AccountRole { get; set; }
    }
}
