using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagementServices.DOTS
{
    public class TreeDataModel
    {
        public string id { get; set; }
        public string title { get; set; }
        public bool Checked { get; set; }
        public bool disabled { get; set; }
        public bool spread { get; set; }
        public TreeDataModel[] children { get; set; }
    }
}
