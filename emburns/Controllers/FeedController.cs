using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace emburns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedController : Controller
    {
        private readonly mokyuContext _context;
        private IEnumerable<RankValue> _ranks;


        public FeedController(mokyuContext context)
        {
            _context = context;
            _ranks = _context.Ranks.Select(r => new RankValue(r)).ToList(); // 

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
                    .Include(f => f.Via)
                    .OrderByDescending(f => f.Id)
                    .Select(f => new FeedBaseQuery(f))
                    .Skip(limit * page).Take(limit).ToListAsync();

                // Ulgy way, I dont find a way to do this much more efficient AT the moment
                for (int i = 0; i < feedItems.Count; i++)
                {
                    feedItems[i].User.RankName = _ranks
                        .Where(r => decimal.Truncate(r.RequiredPoints) == decimal.Truncate(feedItems[i].User.Rank))
                        .ToList().FirstOrDefault().Fullname;
                }

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
        public async Task<IActionResult> Get(int id = 617)
        {
            try
            {

                var feedItems = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .Include(f => f.Via)
                    .Include(f => f.Comments)
                    .ThenInclude(f => f.User)
                    .Where(f => f.Id == id)
                    .Select(f => new FeedBaseQuery(f))
                    .ToListAsync();

                var feedItem = feedItems.FirstOrDefault();

                if (feedItem == null)
                {
                    return NotFound(new { message = "Not Found" });
                }

                feedItem.User.RankName = _ranks
                        .Where(r => decimal.Truncate(r.RequiredPoints) == decimal.Truncate(feedItem.User.Rank))
                        .ToList().FirstOrDefault().Fullname;

                return Ok(feedItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }

        /// <summary>
        /// Search through text|body
        /// </summary>
        /// <param name="query">Search query</param>
        /// <param name="page">Page number, starting by 0</param>
        /// <param name="limit">Limit of feed</param>
        /// <returns>Feed list from last or 500</returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search(string query, int page = 0, int limit = 10)
        {
            try
            {

                var feedItems = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .Include(f => f.Via)
                    .Where(f => f.Text.Contains(query))
                    .OrderByDescending(f => f.Id)
                    .Select(f => new FeedBaseQuery(f))
                    .Skip(limit * page).Take(limit).ToListAsync();

                // Ulgy way, I dont find a way to do this much more efficient AT the moment
                for (int i = 0; i < feedItems.Count; i++)
                {
                    feedItems[i].User.RankName = _ranks
                        .Where(r => decimal.Truncate(r.RequiredPoints) == decimal.Truncate(feedItems[i].User.Rank))
                        .ToList().FirstOrDefault().Fullname;
                }

                return Ok(feedItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }

        /// <summary>
        /// Get feed by user id
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="page">Page number, starting by 0</param>
        /// <param name="limit">Limit of feed</param>
        /// <returns>Feed list from last or 500</returns>
        [HttpGet("userid")]
        public async Task<IActionResult> GetByUserId(int id, int page = 0, int limit = 10)
        {
            try
            {

                var feedItems = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .Include(f => f.Via)
                    .Where(f => f.User.Id == id)
                .OrderByDescending(f => f.Id)
                .Select(f => new FeedBaseQuery(f))
                    .Skip(limit * page).Take(limit).ToListAsync();

                if (feedItems.Count > 0)
                {
                    string rankName = _ranks
                            .Where(r => decimal.Truncate(r.RequiredPoints) == decimal.Truncate(feedItems[0].User.Rank))
                            .ToList().FirstOrDefault().Fullname;

                    // Ulgy way, I dont find a way to do this much more efficient AT the moment
                    for (int i = 0; i < feedItems.Count; i++)
                    {
                        feedItems[i].User.RankName = rankName;
                    }
                }

                return Ok(feedItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }

        /// <summary>
        /// Get feed by username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="page">Page number, starting by 0</param>
        /// <param name="limit">Limit of feed</param>
        /// <returns>Feed list from last or 500</returns>
        [HttpGet("username")]
        public async Task<IActionResult> GetByUserName(string username, int page = 0, int limit = 10)
        {
            try
            {

                var feedItems = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .Include(f => f.Via)
                    .Where(f => f.User.User1 == username)
                .OrderByDescending(f => f.Id)
                .Select(f => new FeedBaseQuery(f))
                    .Skip(limit * page).Take(limit).ToListAsync();

                if (feedItems.Count > 0)
                {
                    string rankName = _ranks
                            .Where(r => decimal.Truncate(r.RequiredPoints) == decimal.Truncate(feedItems[0].User.Rank))
                            .ToList().FirstOrDefault().Fullname;

                    // Ulgy way, I dont find a way to do this much more efficient AT the moment
                    for (int i = 0; i < feedItems.Count; i++)
                    {
                        feedItems[i].User.RankName = rankName;
                    }
                }

                return Ok(feedItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}
