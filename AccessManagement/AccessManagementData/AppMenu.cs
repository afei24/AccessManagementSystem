using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementData
{
    public class AppMenu
    {
        public AppMenu()
        {
            Children = new HashSet<AppMenu>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Route { get; set; }

        public int Order { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public AppMenu Parent { get; set; }


        public ICollection<AppMenu> Children { get; set; }
    }
}
