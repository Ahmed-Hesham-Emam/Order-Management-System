using Shared.Dtos.CustomerDtos;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
    {
    public interface ICustomerService
        {
        Task<CustomerRegisterDto> CreateNewCustomerAsync(CustomerRegisterDto customerRegister);
        Task<IEnumerable<OrdersDto>> GetCustomerOrders(Guid customerId);
        }
    }
