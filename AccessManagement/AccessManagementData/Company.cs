using AccessManagementServices.Common;
using System;
using System.Collections.Generic;

namespace AccessManagementData
{
    [Serializable]
    public partial class Company
    {
        public Company()
        {
            Account = new HashSet<Account>();
            Branch = new HashSet<Branch>();
            Function = new HashSet<Function>();
            Role = new HashSet<Role>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public ComapnyStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }

        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<Branch> Branch { get; set; }
        public virtual ICollection<Function> Function { get; set; }
        public virtual ICollection<Role> Role { get; set; }
    }
}
