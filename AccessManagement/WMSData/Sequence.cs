using System;
using System.Collections.Generic;

namespace WMSData
{
    public partial class Sequence
    {
        public int Id { get; set; }
        public string Sn { get; set; }
        public string TabName { get; set; }
        public int FirstType { get; set; }
        public string FirstRule { get; set; }
        public int? FirstLength { get; set; }
        public int? SecondType { get; set; }
        public string SecondRule { get; set; }
        public int? SecondLength { get; set; }
        public int? ThirdType { get; set; }
        public string ThirdRule { get; set; }
        public int? ThirdLength { get; set; }
        public int? FourType { get; set; }
        public string FourRule { get; set; }
        public int? FourLength { get; set; }
        public string JoinChar { get; set; }
        public string Sample { get; set; }
        public string CurrentValue { get; set; }
        public string Remark { get; set; }
    }
}
