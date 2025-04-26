using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallUsedCars_WebApp.Entities;
using SmallUsedCars_WebApp.Service;
using System.Collections.Generic;

namespace SmallUsedCars_WebApp.Controllers
{
    [Authorize]
    [Route("api/notifications")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        //  전체 고객 조회 (GET)
        [HttpGet("all")]
        public IActionResult GetAllCustomers()
        {
            var customers = _customerService.GetCustomer(null, null, null, null, null);
            return Ok(customers);
        }

        //  특정 고객 조회 (GET)
        [HttpGet("{customerId}")]
        public IActionResult GetCustomerById(int customerId)
        {
            var customers = _customerService.GetCustomer(customerId, null, null, null, null);
            if (customers.Count == 0)
                return NotFound("Customer not found.");

            return Ok(customers[0]);
        }
    }
}
