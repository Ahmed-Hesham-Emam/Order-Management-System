using Domain.Models.CustomerModel;
using Domain.Models.InvoiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
    {
    public class Order : BaseEntity<Guid>
        {
        //public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public OrderStatus Status { get; set; }

        public Invoice Invoice { get; set; }


        }
    }
