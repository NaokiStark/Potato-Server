using emburns.Models;
using emburns.PotatoModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emburns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : Controller
    {
        private readonly mokyuContext _context;

        public FeedController(mokyuContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of feed by descending
        /// </summary>
        /// <param name="page">Page number, starting by 0</param>
        /// <param name="limit">Limit of feed</param>
        /// <returns>Feed list from last or 500</returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList(int page = 0, int limit = 10)
        {
            try
            {

                var feedItems = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .OrderByDescending(f => f.Id)
                    .Select(f => new FeedBaseQuery(f))
                    .Skip(limit * page).Take(limit).ToListAsync();

                return Ok(feedItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }

        /// <summary>
        /// Gets feed by id
        /// </summary>
        /// <param name="id">Feed id</param>
        /// <returns>Ok() or StatusCode(500)</returns>
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {

                var feedItem = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .Where(f => f.Id == id)
                    .Select(f => new FeedBaseQuery(f))
                    .ToListAsync();

                return Ok(feedItem.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}
