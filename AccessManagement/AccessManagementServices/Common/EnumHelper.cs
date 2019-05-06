using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccessManagementServices.Common
{
    public class EnumHelper
    {
        public static List<SelectListItem> EnumToList<T>()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (var e in Enum.GetValues(typeof(T)))
            {
                SelectListItem m = new SelectListItem();
                m.Value = Convert.ToInt32(e).ToString();
                m.Text = e.ToString();
                list.Add(m);
            }
            return list;
        }
    }
}
