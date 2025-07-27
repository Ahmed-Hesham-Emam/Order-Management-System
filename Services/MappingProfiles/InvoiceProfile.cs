using AutoMapper;
using Domain.Models.InvoiceModel;
using Domain.Models.OrderModels;
using Shared.Dtos.InvoiceDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
    {
    public class InvoiceProfile : Profile
        {
        public InvoiceProfile()
            {
            CreateMap<Invoice, InvoiceDto>()
          .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Order.Customer.Name))
          .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Order.OrderItems));

            CreateMap<OrderItem, InvoiceItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            }
        }
    }

