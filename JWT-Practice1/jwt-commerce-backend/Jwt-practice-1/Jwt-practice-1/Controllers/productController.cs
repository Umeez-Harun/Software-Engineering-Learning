using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Security.Claims;

namespace Jwt_practice_1.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class productController : ControllerBase
    {
        private readonly IProductService _productService;
        public productController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> products(ProductRequest request)
        {
            Guid id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            ProductResponse response = await _productService.addProduct(request, id);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> products()
        {
            List<ProductResponse> products = await _productService.getAvailableProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> products(Guid id)
        {
            ProductResponse? product = await _productService.getProduct(id);
            if(product == null)
            {
                return NotFound("Product Could Not Be found");
            }
            return Ok(product);
        }

        [HttpGet("search{value}")]
        public async Task<IActionResult> products(string value)
        {
            List<ProductResponse> products = await _productService.searchProduct(value);
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproducts(Guid id)
        {
            await _productService.deleteProduct(id);
            return NoContent();
        }
    }
}
