using AccessManagementServices.DOTS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.Helper
{
    public static class SessionHelper
    {
        public static void GetAppmenu(ISession Session)
        {
            Session.GetString("CurrentUserId");
            if (Session.Get("account") != null)
            {
                //var account = (AccountViewModel)SerializeHelper.DeserializeWithBinary(Session.Get("account"));

            }
            //else if (!requestPath.Contains("Account/Login"))
            //{
            //}
        }
    }
}
