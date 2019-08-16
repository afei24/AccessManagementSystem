using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Models;
using AccessManagementData;
using AccessManagementServices.Common;
using AccessManagementServices.DOTS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WMSData;

namespace AccessManagement.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccessManagementContext _context;
        private LuJCDBContext _luJCDBContext;
        public AccountController(AccessManagementContext context, LuJCDBContext luJCDBContext)
        {
            _context = context;
            _luJCDBContext = luJCDBContext;
        }
        // GET: api/Account
        [HttpGet]
        public ApiResponse Get()
        {
            if (HttpContext.Session.Get("functions") != null)
            {
                var functions = (List<Function>)SerializeHelper.DeserializeWithBinary(HttpContext.Session.Get("functions"));
                var codes = functions.Select(o=>o.Code).ToList();
                var appmenu = _context.AppMenu.Where(o=>codes.Contains(o.Code));
                return new ApiResponse() { code = 0, data = appmenu };
            }
            else
            {
                return new ApiResponse() { code = -1};
            }
            
        }

        [HttpGet]
        [Route("GetAppMenu")]
        public ApiResponse GetAppMenu()
        {
            if (HttpContext.Session.Get("functions") != null)
            {
                var functions = (List<Function>)SerializeHelper.DeserializeWithBinary(HttpContext.Session.Get("functions"));
                var codes = functions.Select(o => o.Code).ToList();
                var appmenus = _context.AppMenu.Where(o => codes.Contains(o.Code)).ToList();
                var appmenuParentIds = appmenus.Select(o=>o.ParentId).Distinct().Where(o=> o>0).ToList();
                List<AppMenuModel> _appmenus = new List<AppMenuModel>();
                foreach (var appmenuParentId in appmenuParentIds)
                {
                    var parent = _context.AppMenu.FirstOrDefault(o=>o.Id == appmenuParentId);
                    if (parent != null)
                    {
                        AppMenuModel _parent = new AppMenuModel() {
                            Id = parent.Id,
                            Name = parent.Name
                        };
                        var childrens = appmenus.Where(o=>o.ParentId == appmenuParentId).ToList();
                        _parent.AppMenus = childrens.Select(o=>new AppMenuChildren() {
                            Name = o.Name,
                            Route = o.Route,
                            Order = o.Order
                        }).ToList();
                        _appmenus.Add(_parent);
                    }
                }
                
                return new ApiResponse() { code = 0, data = _appmenus };
            }
            else
            {
                return new ApiResponse() { code = -1 };
            }

        }

        // GET: api/Account/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
