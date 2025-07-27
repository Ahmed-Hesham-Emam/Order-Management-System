using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dtos.CustomerDtos;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
    {
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController(IServiceManager serviceManager) : ControllerBase
        {
        [HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserResultDto))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationErrorResponse))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationErrorResponse))]
        public async Task<IActionResult> CreateNewOrder(OrdersDto ordersDto)
            {
            var result = await serviceManager.OrderService.CreateOrder(ordersDto);
            return Ok(result);
            }

        [HttpGet("{orderId:guid}")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrdersDto))]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(Guid orderId)
            {
            var order = await serviceManager.OrderService.GetOrderByIdAsync(orderId);

            if ( order is null )
                return NotFound();

            return Ok(order);
            }


        [HttpGet]
        [Authorize(Roles = "Admin")] // restrict to admin users only
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrdersDto>))]
        public async Task<IActionResult> GetAllOrders()
            {
            var orders = await serviceManager.OrderService.GetAllOrdersAsync();
            return Ok(orders);
            }


        [HttpPut("{orderId}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateOrderStatus(Guid orderId, [FromBody] string newStatus)
            {
            var result = await serviceManager.OrderService.UpdateOrderStatusAsync(orderId, newStatus);
            return result ? Ok("Order status updated.") : BadRequest("Update failed.");
            }

        }
    }
