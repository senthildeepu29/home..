using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.Data;
using ShopForHome.Api.Models;
using System.Globalization;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ShopContext _ctx;
        public UploadController(ShopContext ctx) { _ctx = ctx; }

        // CSV columns: Name,Category,Price,Rating,ImageUrl,Stock
        [HttpPost("products-csv")]
        public async Task<IActionResult> UploadProductsCsv(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("No file");
            using var reader = new StreamReader(file.OpenReadStream());
            string? line;
            int count = 0;
            // skip header if present
            string header = await reader.ReadLineAsync() ?? "";
            if (!header.Contains(",")) { line = header; } else { line = null; }
            if (line != null)
            {
                var p = Parse(line);
                if (p != null) { _ctx.Products.Add(p); count++; }
            }
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var p = Parse(line);
                if (p != null) { _ctx.Products.Add(p); count++; }
            }
            await _ctx.SaveChangesAsync();
            return Ok(new { imported = count });
        }

        private Product? Parse(string line)
        {
            try
            {
                var parts = line.Split(',');
                return new Product
                {
                    Name = parts[0],
                    Category = parts[1],
                    Price = decimal.Parse(parts[2], CultureInfo.InvariantCulture),
                    Rating = decimal.Parse(parts[3], CultureInfo.InvariantCulture),
                    ImageUrl = parts[4],
                    Stock = int.Parse(parts[5])
                };
            }
            catch { return null; }
        }
    }
}
