using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementData
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Function> Functions { get; set; }
    }
}
