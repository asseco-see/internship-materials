using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Product.Commands;
using Product.Database.Repositories;
using Product.Models;
using Product.Services;

namespace Product.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsService _productService;

        public ProductsController(ILogger<ProductsController> logger, IProductsService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortOrder sortOrder)
        {
            page ??= 1;
            pageSize ??= 10;
            return Ok(await _productService.GetProducts(page.Value, pageSize.Value, sortBy, sortOrder));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            return Ok(await _productService.CreateProduct(command));
        }

        [HttpGet("{productCode}")]
        public async Task<IActionResult> GetProductByCode([FromRoute] string productCode)
        {
            var product = await _productService.GetProduct(productCode);

            _logger.LogDebug("Getting product with code: " + productCode);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpDelete("{productCode}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] string productCode)
        {
            var product = await _productService.DeleteProduct(productCode);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
