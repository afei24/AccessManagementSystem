using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccessManagement.ViewCompoents
{
    public class AccountComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int days)
        {
            byte[] accountByte = null;
            if (HttpContext.Session.TryGetValue("account", out accountByte))
            {
                HttpContext.Response.Redirect("/Account/Login");
                return View(new AccountViewModel());
            }
            var account = (AccountViewModel)SerializeHelper.DeserializeWithBinary(accountByte);
            return View(account);
        }
    }
}
