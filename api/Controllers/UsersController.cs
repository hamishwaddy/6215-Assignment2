using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MoviesContext _context;

        public UsersController(MoviesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(int id)
        {   
            Users item = await _context.Users.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users item)
        {
            _context.Users.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
              nameof(GetUsers), 
              item);
        }

        [HttpPut ("{id}")]
         public async Task<IActionResult> PutTodoItem (int id, Users item) {
         if (id != item.Id) {
         return BadRequest ();
         }
         _context.Entry (item).State = EntityState.Modified;
         await _context.SaveChangesAsync ();
         return Content ("User has been updated");
         }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersItem(int id)
        {
            Users model = await _context.Users.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            _context.Users.Remove(model);
            await _context.SaveChangesAsync();

            return Content("User has been removed");
        }
    }
}