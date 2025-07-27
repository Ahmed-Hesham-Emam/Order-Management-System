using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
    {
    public interface IOrderService
        {
        Task<OrdersDto> CreateOrder(OrdersDto orders);
        Task<OrdersDto?> GetOrderByIdAsync(Guid orderId);

        Task<IEnumerable<OrdersDto>> GetAllOrdersAsync();

        Task<bool> UpdateOrderStatusAsync(Guid orderId, string newStatus);

        }
    }
