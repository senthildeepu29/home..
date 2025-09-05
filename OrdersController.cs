using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.Models;
using ShopForHome.Api.Models.Dto;
using ShopForHome.Api.Dtos;


namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ShopContext _context;

        public OrdersController(ShopContext context)
        {
            _context = context;
        }

        // ✅ Place a new order
        [HttpPost("place-order")]
        public IActionResult PlaceOrder(OrderDto dto)
        {
            if (dto == null || dto.Items == null || !dto.Items.Any())
                return BadRequest("Order data is invalid.");

            var order = new Order
            {
                UserId = dto.UserId,
                FullName = dto.FullName,
                Email = dto.Email,
                ShippingAddress = dto.Address,
                City = dto.City,
                Zip = dto.Zip,
                PaymentMethod = dto.PaymentMethod,
                OrderDate = DateTime.UtcNow,
                TotalAmount = dto.TotalAmount,
                Status = "Pending", // default status
                Items = dto.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return Ok(new { message = "Order placed successfully", orderId = order.Id });
        }

        // ✅ Get all orders
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders
                .Include(o => o.Items)
                .Select(o => new
                {
                    o.Id,
                    o.FullName,
                    o.Email,
                    o.ShippingAddress,
                    o.City,
                    o.Zip,
                    o.PaymentMethod,
                    o.TotalAmount,
                    o.Status,
                    o.OrderDate,
                    Items = o.Items.Select(i => new
                    {
                        i.ProductId,
                        i.Quantity,
                        i.Price
                    })
                })
                .ToList();

            return Ok(orders);
        }

        // ✅ Update order status
[HttpPut("{id}")]
public IActionResult UpdateOrderStatus(int id, [FromBody] OrderStatusUpdateDto dto)
{
    var order = _context.Orders.FirstOrDefault(o => o.Id == id);
    if (order == null)
        return NotFound();

    order.Status = dto.Status;  // ✅ make sure Order model has "Status" column
    _context.SaveChanges();

    return Ok(new { message = $"Order #{id} updated to {dto.Status}" });
}


// ✅ Delete order
[HttpDelete("{id}")]
public IActionResult DeleteOrder(int id)
{
    var order = _context.Orders.Include(o => o.Items).FirstOrDefault(o => o.Id == id);
    if (order == null)
        return NotFound();

    _context.OrderItems.RemoveRange(order.Items);
    _context.Orders.Remove(order);
    _context.SaveChanges();

    return Ok(new { message = $"Order #{id} deleted" });
}


    }
}
