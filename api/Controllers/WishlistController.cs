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
    public class WishlistController : ControllerBase
    {
        private readonly MoviesContext _context;

        public WishlistController(MoviesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wishlist>>> GetWishlist()
        {
            return await _context.Wishlist.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Wishlist>> GetWishlist(long id)
        {   
            Wishlist item = await _context.Wishlist.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Wishlist>> PostWishlist(Wishlist item)
        {
            _context.Wishlist.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
              nameof(GetWishlist), 
              item);
        }

        [HttpPut ("{id}")]
         public async Task<IActionResult> PutTodoItem (int id, Wishlist item) {
         if (id != item.Id) {
         return BadRequest ();
         }
         _context.Entry (item).State = EntityState.Modified;
         await _context.SaveChangesAsync ();
         return Content ("Wishlist has been updated");
         }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWishlistItem(short id)
        {
            Wishlist model = await _context.Wishlist.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            _context.Wishlist.Remove(model);
            await _context.SaveChangesAsync();

            return Content("Wishlist has been removed");
        }
    }
}