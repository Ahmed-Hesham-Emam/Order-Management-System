using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos.InvoiceDtos
    {
    public class InvoiceDto
        {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }

        public string CustomerName { get; set; }
        public List<InvoiceItemDto> Items { get; set; }
        }
    }
