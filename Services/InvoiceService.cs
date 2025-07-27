using AutoMapper;
using Domain.Contracts;
using Domain.Models.InvoiceModel;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Shared.Dtos.InvoiceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
    {
    public class InvoiceService(IUnitOfWork unitOfWork, IMapper mapper) : IInvoiceService
        {
        public async Task<IEnumerable<InvoiceDto>> GetAllInvoicesAsync()
            {
            var invoices = await unitOfWork
                .GetRepository<Invoice, Guid>()
                .GetAllAsync(query =>
                    query.Include(i => i.Order)
                         .ThenInclude(o => o.Customer));

            return mapper.Map<IEnumerable<InvoiceDto>>(invoices);
            }

        public async Task<InvoiceDto?> GetInvoiceByIdAsync(Guid id)
            {
            var invoices = await unitOfWork
                .GetRepository<Invoice, Guid>()
                .GetAllAsync(query =>
                    query.Include(i => i.Order)
                         .ThenInclude(o => o.Customer));

            var invoice = invoices.FirstOrDefault(i => i.Id == id);
            return invoice is null ? null : mapper.Map<InvoiceDto>(invoice);
            }
        }
    }
