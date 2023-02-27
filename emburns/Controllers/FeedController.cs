using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.User)

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.ReactionNavigation)

                    .OrderByDescending(f => f.Id)
                    .Select(f => new FeedBaseQuery(f))
                    .Skip(limit * page).Take(limit).ToListAsync();

                feedItems.ForEach(f => f.User.FetchUserRank(_ranks));

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

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.User)

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.ReactionNavigation)

                    .Where(f => f.Id == id)
                    .Select(f => new FeedBaseQuery(f))
                    .ToListAsync();

                var feedItem = feedItems.FirstOrDefault();

                if (feedItem == null)
                {
                    return NotFound(new { message = "Not Found" });
                }

                feedItem.User.FetchUserRank(_ranks);
                feedItem.Comments.ForEach(c => c.User.FetchUserRank(_ranks));

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

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.User)

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.ReactionNavigation)

                    .Where(f => f.Text.Contains(query))
                    .OrderByDescending(f => f.Id)
                    .Select(f => new FeedBaseQuery(f))
                    .Skip(limit * page).Take(limit).ToListAsync();

                feedItems.ForEach(f => f.User.FetchUserRank(_ranks));

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

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.User)

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.ReactionNavigation)

                    .Where(f => f.User.Id == id)
                    .OrderByDescending(f => f.Id)
                    .Select(f => new FeedBaseQuery(f))
                    .Skip(limit * page).Take(limit).ToListAsync();

                feedItems.ForEach(f => f.User.FetchUserRank(_ranks));

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

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.User)

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.ReactionNavigation)

                    .Where(f => f.User.User1 == username)
                .OrderByDescending(f => f.Id)
                .Select(f => new FeedBaseQuery(f))
                    .Skip(limit * page).Take(limit).ToListAsync();

                feedItems.ForEach(f => f.User.FetchUserRank(_ranks));

                return Ok(feedItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}
