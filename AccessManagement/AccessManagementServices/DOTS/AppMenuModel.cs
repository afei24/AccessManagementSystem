using AccessManagementData;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.DOTS
{
    public  class AppMenuModel
    {
        public AppMenuModel()
        {
            AppMenus = new List<AppMenuChildren>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<AppMenuChildren> AppMenus { get; set; }
        public bool IsItemed { get; set; }
    }

    public class AppMenuChildren
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public int Order { get; set; }
    }
}
