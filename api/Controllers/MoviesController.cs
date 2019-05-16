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
    public class MoviesController : ControllerBase
    {
        private readonly MoviesContext _context;

        public MoviesController(MoviesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movies>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movies>> GetMovies(long id)
        {   
            Movies item = await _context.Movies.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Movies>> PostMODEL(Movies item)
        {
            _context.Movies.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
              nameof(GetMovies), 
              item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoviesItem,(short id, Movies item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Content("Movie has been updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoviesItem(short id)
        {
            Movies model = await _context.Movies.FindAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(model);
            await _context.SaveChangesAsync();

            return Content("Movie has been removed");
        }
    }
}