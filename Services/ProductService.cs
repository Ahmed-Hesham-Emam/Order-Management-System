using AutoMapper;
using Domain.Contracts;
using Domain.Models.ProductModel;
using Services.Abstractions;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
    {
    public class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
        {

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
            {
            var products = await unitOfWork.GetRepository<Product, Guid>().GetAllAsync();
            return mapper.Map<IEnumerable<ProductDto>>(products);
            }

        public async Task<ProductDto> GetProductByIdAsync(Guid productId)
            {
            var product = await unitOfWork.GetRepository<Product, Guid>().GetByIdAsync(productId);
            if ( product == null )
                throw new Exception("Product not found");
            return mapper.Map<ProductDto>(product);
            }

        public async Task<ProductDto> CreateProductAsync(ProductDto dto)
            {
            var product = mapper.Map<Product>(dto);
            product.Id = Guid.NewGuid();
            await unitOfWork.GetRepository<Product, Guid>().AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<ProductDto>(product);
            }

        public async Task<bool> UpdateProductAsync(Guid productId, ProductDto dto)
            {
            var repo = unitOfWork.GetRepository<Product, Guid>();
            var product = await repo.GetByIdAsync(productId);
            if ( product == null ) return false;

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.Stock = dto.StockQuantity;

            await unitOfWork.SaveChangesAsync();
            return true;
            }

        }
    }
