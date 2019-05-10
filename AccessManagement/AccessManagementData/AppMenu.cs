using System;
using System.Collections.Generic;

namespace AccessManagementData
{
    public partial class AppMenu
    {
        public AppMenu()
        {
            InverseParent = new HashSet<AppMenu>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public int Order { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }

        public virtual AppMenu Parent { get; set; }
        public virtual ICollection<AppMenu> InverseParent { get; set; }
    }
}
