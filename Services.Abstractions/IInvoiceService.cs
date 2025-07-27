using Shared.Dtos.InvoiceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
    {
    public interface IInvoiceService
        {
        Task<InvoiceDto> GetInvoiceByIdAsync(Guid invoiceId);
        Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync();
        }
    }
