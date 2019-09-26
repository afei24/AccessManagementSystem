using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccessManagement.Models;
using AccessManagementServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccessManagement.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImsController : ControllerBase
    {
        private SupplierServices _supplierServices;
        private CustomerServices _customerServices;
        public ImsController(SupplierServices supplierServices, CustomerServices customerServices)
        {
            _supplierServices = supplierServices;
            _customerServices = customerServices;
        }
        [HttpGet]
        [Route("GetSupplier/{id}")]
        public async Task<ApiResponse> GetSupplier(int id)
        {
            try
            {
                var vm = await _supplierServices.GetById(id);
                return new ApiResponse() { code = 0, data = vm };
            }
            catch (Exception ex)
            {
                return new ApiResponse() { code = -1,message = ex.Message };
            }
        }

        [HttpGet]
        [Route("GetCustomer/{id}")]
        public async Task<ApiResponse> GetCustomer(int id)
        {
            try
            {
                var vm = await _customerServices.GetById(id);
                return new ApiResponse() { code = 0, data = vm };
            }
            catch (Exception ex)
            {
                return new ApiResponse() { code = -1, message = ex.Message };
            }
        }
    }
}