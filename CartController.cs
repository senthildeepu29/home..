using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.Data;
using ShopForHome.Api.Models;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ShopContext _ctx;
        public CartController(ShopContext ctx) { _ctx = ctx; }

        [HttpGet("{userId:int}")]
        public IActionResult GetCart(int userId)
        {
            var items = _ctx.CartItems.Where(c => c.UserId == userId).ToList();
            return Ok(items);
        }

        [HttpPost("{userId:int}/add/{productId:int}")]
        public IActionResult Add(int userId, int productId)
        {
            var item = _ctx.CartItems.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
            if (item == null) { item = new CartItem { UserId = userId, ProductId = productId, Quantity = 1 }; _ctx.CartItems.Add(item); }
            else { item.Quantity += 1; }
            _ctx.SaveChanges();
            return Ok(item);
        }

        [HttpPost("{userId:int}/update/{productId:int}")]
        public IActionResult UpdateQty(int userId, int productId, [FromQuery] int qty = 1)
        {
            var item = _ctx.CartItems.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
            if (item == null) return NotFound();
            item.Quantity = Math.Max(1, qty);
            _ctx.SaveChanges();
            return Ok(item);
        }

        [HttpDelete("{userId:int}/remove/{productId:int}")]
        public IActionResult Remove(int userId, int productId)
        {
            var item = _ctx.CartItems.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
            if (item == null) return NotFound();
            _ctx.CartItems.Remove(item); _ctx.SaveChanges();
            return NoContent();
        }
    }
}
