using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.Dtos.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
    {
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager serviceManager) : ControllerBase
        {
        [HttpGet]
        public async Task<IActionResult> GetAll()
            {
            var result = await serviceManager.ProductService.GetAllProductsAsync();
            return Ok(result);
            }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(Guid productId)
            {
            var result = await serviceManager.ProductService.GetProductByIdAsync(productId);
            return Ok(result);
            }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(ProductDto dto)
            {
            var result = await serviceManager.ProductService.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetById), new { productId = result.Id }, result);
            }

        [HttpPut("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid productId, ProductDto dto)
            {
            var result = await serviceManager.ProductService.UpdateProductAsync(productId, dto);
            return result ? Ok("Product updated.") : NotFound("Product not found.");
            }
        }
    }
