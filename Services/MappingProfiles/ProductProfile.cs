using AutoMapper;
using Domain.Models.ProductModel;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
    {
    public class ProductProfile : Profile
        {
        public ProductProfile()
            {
            CreateMap<Product, ProductDto>().ReverseMap();
            }
        }
    }
