using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
    {
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController(IServiceManager serviceManager) : ControllerBase
        {
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllInvoices()
            {
            var invoices = await serviceManager.InvoiceService.GetAllInvoicesAsync();
            return Ok(invoices);
            }

        // GET: api/invoices/{id}
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInvoiceById(Guid id)
            {
            var invoice = await serviceManager.InvoiceService.GetInvoiceByIdAsync(id);
            if ( invoice is null )
                return NotFound($"Invoice with ID {id} not found.");

            return Ok(invoice);
            }
        }
    }
