using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HomeWork2_JWTToken_ProductManagementAPI.Models;

namespace HomeWork2_JWTToken_ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private static readonly List<Product> products = new List<Product>();
        private static int nextId = 1;
        [HttpGet("GetById/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost("Create")]
        public IActionResult CreateProduct(Product product)
        {
            if(string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError(
                    nameof(product.Name),
                    $"{nameof(product.Name)} cannot be null or empty.");
            }
            if(product.Price <=0)
            {
                ModelState.AddModelError(
                    nameof(product.Price),
                    $"{product.Price} must be greater than 0.");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            product.Id = nextId++;
            products.Add(product);
            return CreatedAtAction(nameof(GetProductById) , new {id = product.Id} , product);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return NoContent();
        }
    }
}
