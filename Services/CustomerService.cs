using Domain.Contracts;
using Domain.Models.CustomerModel;
using Domain.Models.IdentityModel;
using Domain.Models.OrderModels;
using Domain.Models.ProductModel;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared.Dtos.CustomerDtos;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
    {
    public class CustomerService(IUnitOfWork unitOfWork, UserManager<User> userManager) : ICustomerService
        {
        public async Task<CustomerRegisterDto> CreateNewCustomerAsync(CustomerRegisterDto customerRegister)
            {
            var user = await userManager.FindByEmailAsync(customerRegister.Email);
            var customer = new Customer
                {
                UserId = user.Id,
                Id = Guid.NewGuid(),
                Name = customerRegister.Name
                ,
                Email = customerRegister.Email,

                };

            await userManager.AddToRoleAsync(user, "Customer");
            await unitOfWork.GetRepository<Customer, Guid>().AddAsync(customer);
            await unitOfWork.SaveChangesAsync();
            customerRegister.Id = customer.Id;
            return customerRegister;
            }

        public async Task<IEnumerable<OrdersDto>> GetCustomerOrders(Guid customerId)
            {

            var customer = await unitOfWork.GetRepository<Customer, Guid>().GetByIdAsync(customerId);

            if ( customer is null || customer.Orders is null )
                return Enumerable.Empty<OrdersDto>();

            var ProductIds = customer.Orders.SelectMany(o => o.OrderItems)
                .Select(o => o.ProductId)
                .Distinct()
                .ToList();

            var Allproducts = await unitOfWork.GetRepository<Product, Guid>().GetAllAsync();

            var product = Allproducts.Where(p => ProductIds.Contains(p.Id))
                .ToDictionary(p => p.Id, p => p.Name);

            var result = customer.Orders.Select(o => new OrdersDto
                {
                OrderId = o.Id,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString(),
                PaymentMethod = o.PaymentMethod.ToString(),
                OrderItems = o.OrderItems.Select(oi => new OrderItemDto
                    {
                    ProductName = product.TryGetValue(oi.ProductId, out var name) ? name : "Unknown Product",
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice,
                    Discount = oi.Discount

                    }).ToList()


                });
            return result;

            }
        }
    }
