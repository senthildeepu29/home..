using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.Data;
using ShopForHome.Api.Models;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController : ControllerBase
    {
        private readonly ShopContext _ctx;
        public WishlistController(ShopContext ctx) { _ctx = ctx; }

        [HttpGet("{userId:int}")]
        public IActionResult Get(int userId) => Ok(_ctx.WishlistItems.Where(w => w.UserId == userId).ToList());

        [HttpPost("{userId:int}/add/{productId:int}")]
        public IActionResult Add(int userId, int productId)
        {
            if (_ctx.WishlistItems.Any(w => w.UserId == userId && w.ProductId == productId)) return Conflict("Already in wishlist");
            var item = new WishlistItem { UserId = userId, ProductId = productId };
            _ctx.WishlistItems.Add(item); _ctx.SaveChanges(); return Ok(item);
        }

        [HttpDelete("{userId:int}/remove/{productId:int}")]
        public IActionResult Remove(int userId, int productId)
        {
            var item = _ctx.WishlistItems.FirstOrDefault(w => w.UserId == userId && w.ProductId == productId);
            if (item == null) return NotFound();
            _ctx.WishlistItems.Remove(item); _ctx.SaveChanges(); return NoContent();
        }
    }
}
