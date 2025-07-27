using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.OrderDtos
    {
    public class OrdersDto
        {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        }
    }
