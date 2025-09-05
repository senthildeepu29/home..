using System.Globalization;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.Models;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ShopContext _context;

        public ProductsController(ShopContext ctx)
        {
            _context = ctx;
        }

        // GET: api/products
        [HttpGet]
        public IActionResult GetAll([FromQuery] string? category, [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice, [FromQuery] decimal? minRating)
        {
            var q = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
                q = q.Where(p => p.Category == category);

            if (minPrice.HasValue)
                q = q.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                q = q.Where(p => p.Price <= maxPrice.Value);

            if (minRating.HasValue)
                q = q.Where(p => p.Rating >= minRating.Value);

            return Ok(q.ToList());
        }

        // GET: api/products/{id}
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/products
        [HttpPost]
        public IActionResult Create(Product p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = p.Id }, p);
        }

        // PUT: api/products/{id}
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Product p)
        {
            if (id != p.Id) return BadRequest();

            _context.Entry(p).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/products/{id}
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var p = _context.Products.Find(id);
            if (p == null) return NotFound();

            _context.Products.Remove(p);
            _context.SaveChanges();
            return NoContent();
        }

        // POST: api/products/upload-csv
        [HttpPost("upload-csv")]
public async Task<IActionResult> UploadCsv(IFormFile file)
{
    if (file == null || file.Length == 0)
        return BadRequest("File not selected");

    using var reader = new StreamReader(file.OpenReadStream());

    var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
    {
        PrepareHeaderForMatch = args => args.Header.ToLower(),
        MissingFieldFound = null, // ignore missing fields
        HeaderValidated = null    // ignore header validation
    };

    using var csv = new CsvReader(reader, config);

    List<Product> records;
    try
    {
        records = csv.GetRecords<Product>().ToList();
    }
    catch (Exception ex)
    {
        return BadRequest($"CSV PARSE ERROR: {ex.Message}");
    }

    try
    {
        await _context.Products.AddRangeAsync(records);
        await _context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
        return StatusCode(500, new
        {
            Error = ex.Message,
            Inner = ex.InnerException?.Message
        });
    }

    return Ok(new { Count = records.Count });
}


        // GET: api/products/stock
       [HttpGet("stock")]
public IActionResult GetStock()
{
    var products = _context.Products
        .Select(p => new
        {
            p.Id,
            p.Name,
            p.Stock,
            LowStock = p.Stock < 10
        })
        .ToList();

    return Ok(products);
}

    }
}
