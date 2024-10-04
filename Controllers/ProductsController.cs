using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApiProject.Models;
using MyApiProject.Services;
namespace MyApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productservice;
        public ProductsController(IProductService productService)
        {
            _productservice = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productservice.GetProductAsync();
            if (products != null)
            {
                return Ok(products);
            }
            return NotFound("No data present");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById([FromRoute] int id)
        {
            var product = await _productservice.GetProductByIdAsync(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound("No Data Found");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Product product)
        {
            await _productservice.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductID }, product);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            product.ProductID = id;
            var updated = await _productservice.UpdateProductAsync(product);
            if (updated == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            var deleted = await _productservice.DeleteProductAsync(id);
            if (deleted == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}