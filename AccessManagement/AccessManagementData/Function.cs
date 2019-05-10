using System;
using System.Collections.Generic;

namespace AccessManagementData
{
    public partial class Function
    {
        public Function()
        {
            AccountFunction = new HashSet<AccountFunction>();
            FunctionRole = new HashSet<FunctionRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        public int Type { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string OpName1 { get; set; }
        public string OpName2 { get; set; }
        public string OpName3 { get; set; }
        public string OpName4 { get; set; }
        public string OpName5 { get; set; }
        public string OpName6 { get; set; }
        public string OpName7 { get; set; }
        public string OpName8 { get; set; }
        public string OpName9 { get; set; }
        public string OpName10 { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<AccountFunction> AccountFunction { get; set; }
        public virtual ICollection<FunctionRole> FunctionRole { get; set; }
    }
}
