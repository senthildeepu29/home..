using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ShopContext _context;
        public ReportsController(ShopContext ctx) { _context = ctx; }

        // GET: api/reports/sales?start=2025-09-01&end=2025-09-05
        [HttpGet("sales")]
        public IActionResult GetSalesReport([FromQuery] DateTime start, [FromQuery] DateTime end)
        {
            var report = _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Where(o => o.OrderDate >= start && o.OrderDate <= end)
                .Select(o => new
                {
                    o.Id,
                    o.OrderDate,
                    o.TotalAmount,
                    Items = o.Items.Select(i => new
                    {
                        i.ProductId,
                        ProductName = i.Product!.Name,
                        i.Quantity,
                        i.UnitPrice,
                        LineTotal = i.Quantity * i.UnitPrice
                    })
                })
                .ToList();

            var totalSales = report.Sum(o => o.TotalAmount);

            return Ok(new
            {
                StartDate = start,
                EndDate = end,
                TotalOrders = report.Count,
                TotalSales = totalSales,
                Orders = report
            });
        }
    }
}
