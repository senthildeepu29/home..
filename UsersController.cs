using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopForHome.Api.Data;
using ShopForHome.Api.Models;
using ShopForHome.Api.Utils;

namespace ShopForHome.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ShopContext _ctx;
        public UsersController(ShopContext ctx) { _ctx = ctx; }

        [HttpGet]
        public IActionResult GetAll() => Ok(_ctx.Users.Select(u => new { u.Id, u.FullName, u.Email, u.Role }).ToList());

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var u = _ctx.Users.Find(id);
            if (u == null) return NotFound();
            return Ok(new { u.Id, u.FullName, u.Email, u.Role });
        }

        [HttpPost]
        public IActionResult Create(User u)
        {
            u.PasswordHash = PasswordHasher.Hash("ChangeMe@123");
            _ctx.Users.Add(u); _ctx.SaveChanges();
            return CreatedAtAction(nameof(Get), new { id = u.Id }, new { u.Id, u.FullName, u.Email, u.Role });
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, User update)
        {
            var u = _ctx.Users.Find(id);
            if (u == null) return NotFound();
            u.FullName = update.FullName;
            u.Email = update.Email;
            u.Role = update.Role;
            _ctx.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var u = _ctx.Users.Find(id);
            if (u == null) return NotFound();
            _ctx.Users.Remove(u); _ctx.SaveChanges();
            return NoContent();
        }
    }
}
