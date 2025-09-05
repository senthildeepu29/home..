using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.Models;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly ShopContext _context;
        public AdminController(ShopContext context)
        {
            _context = context;
        }

        // Get all products
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        // Add product
        [HttpPost("products")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // Update product
        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Category = product.Category;
            existing.ImageUrl = product.ImageUrl;
            existing.Stock = product.Stock;
            existing.Rating = product.Rating;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        // Delete product
        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // View orders
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _context.Orders.Include(o => o.Items).ToListAsync();
            return Ok(orders);
        }
    }
}
