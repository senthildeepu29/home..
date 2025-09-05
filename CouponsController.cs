// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using ShopForHome.Api.Data;
// using ShopForHome.Api.Models;

// namespace ShopForHome.Api.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class CouponsController : ControllerBase
//     {
//         private readonly ShopContext _context;
//         public CouponsController(ShopContext context)
//         {
//             _context = context;
//         }

//         // Create a coupon
//         [HttpPost]
//         public async Task<IActionResult> CreateCoupon(Coupon coupon)
//         {
//             _context.Coupons.Add(coupon);
//             await _context.SaveChangesAsync();
//             return Ok(coupon);
//         }

//         // Assign coupon to specific users
//         [HttpPost("{couponId}/assign")]
//         public async Task<IActionResult> AssignCoupon(int couponId, [FromBody] List<int> userIds)
//         {
//             var coupon = await _context.Coupons.FindAsync(couponId);
//             if (coupon == null) return NotFound("Coupon not found");

//             foreach (var userId in userIds)
//             {
//                 if (!_context.UserCoupons.Any(uc => uc.CouponId == couponId && uc.UserId == userId))
//                 {
//                     _context.UserCoupons.Add(new UserCoupon
//                     {
//                         CouponId = couponId,
//                         UserId = userId
//                     });
//                 }
//             }

//             await _context.SaveChangesAsync();
//             return Ok(new { Message = "Coupon assigned", CouponId = couponId, Users = userIds });
//         }

//         // Get coupons of a user
//         [HttpGet("user/{userId}")]
//         public async Task<IActionResult> GetUserCoupons(int userId)
//         {
//             var coupons = await _context.UserCoupons
//                 .Where(uc => uc.UserId == userId)
//                 .Include(uc => uc.Coupon)
//                 .Select(uc => uc.Coupon)
//                 .ToListAsync();

//             return Ok(coupons);
//         }
//     }
// }


using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.Data;
using ShopForHome.Api.Models;
using System.Linq;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CouponsController : ControllerBase
    {
        private readonly ShopContext _context;

        public CouponsController(ShopContext context)
        {
            _context = context;
        }

        // ✅ Get all coupons
        [HttpGet]
        public IActionResult GetCoupons()
        {
            var coupons = _context.Coupons.ToList();
            return Ok(coupons);
        }

        // ✅ Create a new coupon
        [HttpPost]
public async Task<IActionResult> CreateCoupon([FromBody] Coupon coupon)
{
    if (coupon == null || string.IsNullOrEmpty(coupon.Code))
        return BadRequest("Invalid coupon data");

    _context.Coupons.Add(coupon);
    await _context.SaveChangesAsync();

    return Ok(coupon);
}


        // ✅ Delete coupon
        [HttpDelete("{id}")]
        public IActionResult DeleteCoupon(int id)
        {
            var coupon = _context.Coupons.Find(id);
            if (coupon == null)
                return NotFound();

            _context.Coupons.Remove(coupon);
            _context.SaveChanges();

            return Ok(new { message = "Coupon deleted successfully" });
        }
    }
}
