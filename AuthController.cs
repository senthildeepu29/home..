using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using ShopForHome.Api.Data;
using ShopForHome.Api.DTOs;
using ShopForHome.Api.Models;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ShopContext _ctx;
        public AuthController(ShopContext ctx) { _ctx = ctx; }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            if (_ctx.Users.Any(u => u.Email == dto.Email))
                return BadRequest(new { message = "❌ Email already exists" });

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password), // ✅ hash into Password
                Role = "User"
            };

            _ctx.Users.Add(user);
            _ctx.SaveChanges();

            return Ok(new { user.Id, user.Email, user.FullName });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _ctx.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null)
                return Unauthorized(new { message = "❌ Invalid email or password" });

            // ✅ check hashed password
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Unauthorized(new { message = "❌ Invalid email or password" });

            return Ok(new
            {
                id = user.Id,
                fullName = user.FullName,
                email = user.Email,
                role = user.Role
            });
        }
    }
}
