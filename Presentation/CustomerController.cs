using Domain.Models.CustomerModel;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dtos.CustomerDtos;
using Shared.Dtos.OrderDtos;
using Shared.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
    {
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController(IServiceManager serviceManager) : ControllerBase
        {
        [HttpPost]

        public async Task<IActionResult> CreateNewCustomer(CustomerRegisterDto registerDto)
            {
            var result = await serviceManager.CustomerService.CreateNewCustomerAsync(registerDto);
            return Ok(result);
            }

        [HttpGet("{customerId}/orders")]

        public async Task<IActionResult> GetCustomerOrders(Guid customerId)
            {
            var result = await serviceManager.CustomerService.GetCustomerOrders(customerId);
            return Ok(result);

            }
        }
    }
