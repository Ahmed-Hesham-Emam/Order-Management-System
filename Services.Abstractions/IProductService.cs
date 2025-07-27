using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
    {
    public interface IProductService
        {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(Guid productId);
        Task<ProductDto> CreateProductAsync(ProductDto dto);
        Task<bool> UpdateProductAsync(Guid productId, ProductDto dto);
        }
    }
