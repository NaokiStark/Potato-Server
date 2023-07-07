using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections.Generic;

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
        /// <param name="type">Feed type = recent (can be recent, trending, media)</param>
        /// <param name="page">Page number, starting by 0</param>
        /// <param name="limit">Limit of feed</param>
        /// <returns>Feed list from last or 500</returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList(string type = "recent", int page = 0, int limit = 10)
        {
            try
            {

                List<FeedBaseQuery> feedItems = new List<FeedBaseQuery>();
                var query = _context.Feeds.AsNoTracking()
                            .Include(f => f.User)
                            .Include(f => f.Via)

                            .Include(f => f.LovesNavigation)
                            .ThenInclude(l => l.User)

                            .Include(f => f.LovesNavigation)
                            .ThenInclude(l => l.ReactionNavigation);


                IQueryable<Feed> filteredQuery = query.Where(f => f.Status == false && f.ParentId == 0);
                // What a weird way to make filters, but it works©
                switch (type)
                {
                    case "trending":
                        DateTime lastestWeeks = DateTime.Today.Subtract(TimeSpan.FromDays(365 * 6));
                        filteredQuery = filteredQuery.Where(f => f.Created >= lastestWeeks)
                            .OrderByDescending((f) => f.LovesNavigation.Count);
                        break;
                    case "media":
                        filteredQuery = filteredQuery.Where(f => f.AttachmentType > 0)
                            .OrderByDescending((f) => f.Id); 
                        break;
                    default:
                        filteredQuery = filteredQuery.OrderByDescending((f) => f.Id);
                        break;
                }

                var fbq = filteredQuery.Select(f => new FeedBaseQuery(f))
                        .Skip(limit * page).Take(limit); ;


                feedItems = await fbq.ToListAsync();

                feedItems.ForEach(f => f.User.FetchUserRank(_ranks));

                return Ok(feedItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }

        /// <summary>
        /// Gets latest feed by user
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("newsfeed")]
        public async Task<IActionResult> GetNewsFeed(int page = 0, int limit = 10)
        {
            try
            {
                // get userdata

                string username = User.Identity.Name;

                var usersList = await _context.Users
                        .OrderBy(u => u.Id)
                        .Where(u => u.User1 == username)
                        .Select(u => new { u.Id })
                        .Take(2)
                        .ToListAsync();

                var userItem = usersList.FirstOrDefault();

                if (userItem == null)
                {
                    throw new Exception("Bad Request");
                }

                var follows = await _context.Follows
                    .Where(f => f.Follower == userItem.Id)
                    .Select(f => f.Following)
                    .ToListAsync();

                var feedItems = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .Include(f => f.Via)

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.User)

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.ReactionNavigation)

                    .OrderByDescending(f => f.Id)
                    .Where(f => f.Status == false && (follows.Contains(f.Userid) || f.Userid == userItem.Id))

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
                    .Where(f => f.Status == false)
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

                    .Where(f => f.Text.ToLower().Contains(query.ToLower()))
                    .Where(f => f.Status == false && f.ParentId == 0)
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
                    .Where(f => f.Status == false)
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
                    .Where(f => f.Status == false)
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
