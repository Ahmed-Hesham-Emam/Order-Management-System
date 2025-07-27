using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
    {
    public interface IServiceManager
        {
        IAuthService AuthService { get; }
        ICustomerService CustomerService { get; }
        IOrderService OrderService { get; }

        IProductService ProductService { get; }

        IInvoiceService InvoiceService { get; }
        }
    }
