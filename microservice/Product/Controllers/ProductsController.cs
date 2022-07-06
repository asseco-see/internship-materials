using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Commands;
using Product.Models;
using Product.Services;

namespace Product.Controllers
{
    [Route("v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductsService productsService, ILogger<ProductsController> logger)
        {
            _productsService = productsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortOrder sortOrder)
        {
            page = page ?? 1;
            pageSize = pageSize ?? 10;
            _logger.LogInformation("Returning {page}. page of products", page);
            var result = await _productsService.GetProducts(page.Value, pageSize.Value, sortBy, sortOrder);
            return Ok(result);
        }

        [HttpGet("{productCode}")]
        public async Task<IActionResult> GetProduct([FromRoute] string productCode)
        {
            var product = await _productsService.GetProduct(productCode);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            var result = await _productsService.CreateProduct(command);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpDelete("{productCode}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] string productCode)
        {
            var result = await _productsService.DeleteProduct(productCode);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
