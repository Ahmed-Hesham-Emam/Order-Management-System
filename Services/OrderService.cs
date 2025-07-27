using Domain.Contracts;
using Domain.Models.CustomerModel;
using Domain.Models.OrderModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Shared.Dtos.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
    {
    public class OrderService(IUnitOfWork unitOfWork) : IOrderService
        {
        public async Task<OrdersDto> CreateOrder([FromBody] OrdersDto ordersDto)
            {

            if ( ordersDto is null || ordersDto.OrderItems is null || !ordersDto.OrderItems.Any() )
                throw new Exception("Invalid order.");

            var order = new Order
                {
                Id = Guid.NewGuid(),
                CustomerId = ordersDto.CustomerId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = ordersDto.TotalAmount,
                OrderItems = ordersDto.OrderItems.Select(item => new OrderItem
                    {
                    Id = Guid.NewGuid(),
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount,

                    }).ToList(),
                PaymentMethod = Enum.Parse<PaymentMethod>(ordersDto.PaymentMethod),
                Status = Enum.Parse<OrderStatus>(ordersDto.Status)
                };
            order.TotalAmount = order.OrderItems.Sum(item => item.UnitPrice * item.Quantity - item.Discount);

            ordersDto = new OrdersDto
                {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                PaymentMethod = order.PaymentMethod.ToString(),
                Status = order.Status.ToString(),
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                    {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount
                    }).ToList()
                };


            await unitOfWork.GetRepository<Order, Guid>().AddAsync(order);
            await unitOfWork.SaveChangesAsync();

            return ordersDto;
            }

        public async Task<OrdersDto?> GetOrderByIdAsync(Guid orderId)
            {
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(orderId);
            if ( order is null )
                return null;

            var result = new OrdersDto
                {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                PaymentMethod = order.PaymentMethod.ToString(),
                Status = order.Status.ToString(),
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                    {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount
                    }).ToList()
                };
            return result;
            }

        public async Task<IEnumerable<OrdersDto>> GetAllOrdersAsync()
            {
            var orders = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(o => o.Include(oi => oi.OrderItems));

            var result = orders.Select(order => new OrdersDto
                {
                OrderId = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                PaymentMethod = order.PaymentMethod.ToString(),
                Status = order.Status.ToString(),
                OrderItems = order.OrderItems.Select(item => new OrderItemDto
                    {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Discount = item.Discount
                    }).ToList()
                });
            return result;
            }


        public async Task<bool> UpdateOrderStatusAsync(Guid orderId, string newStatus)
            {
            var orderRepo = unitOfWork.GetRepository<Order, Guid>();
            var order = await orderRepo.GetByIdAsync(orderId);


            if ( order is null )
                throw new Exception("Order not found");

            if ( !Enum.TryParse<OrderStatus>(newStatus, out var status) )
                throw new Exception("Invalid order status");

            order.Status = status;
            await unitOfWork.SaveChangesAsync();
            return true;

            }

        }
    }
