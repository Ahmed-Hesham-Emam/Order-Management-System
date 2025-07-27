using Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.InvoiceModel
    {
    public class Invoice : BaseEntity<Guid>
        {
        //public Guid InvoiceId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        }
    }
