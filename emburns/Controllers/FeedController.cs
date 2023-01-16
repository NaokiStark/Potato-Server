using emburns.Models;
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
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<IActionResult> GetList(int page = 0, int limit = 10)
        {
            try
            {

                var feedItems = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .Select(f => new
                    {
                        f.Id,
                        f.Userid,
                        f.Text,
                        f.Attachment,
                        f.AttachmentType,
                        f.ViaId,
                        f.ParentId,
                        f.Created,
                        f.Status,
                        f.Loves,
                        f.Nsfw,
                        f.Sticky,
                        UserInfo = new
                        {
                            User = f.User.User1,
                            f.User.Name,
                            f.User.Lastname,
                            f.User.Avatar,
                            f.User.Background,
                            f.User.Cover,
                            f.User.Country,
                            f.User.Quote,
                            f.User.Rank,
                            f.User.Donation,
                            user_created = f.User.Created,
                            RankName = _context.Ranks.Where(r => f.User.Rank >= r.RequiredPoints).FirstOrDefault().Fullname,
                        }
                    })
                    .OrderByDescending(f => f.Id)
                    .Skip(limit * page).Take(limit).ToListAsync();

                return Ok(feedItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }


        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            try
            {

                var feedItem = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .Select(f => new
                    {
                        f.Id,
                        f.Userid,
                        f.Text,
                        f.Attachment,
                        f.AttachmentType,
                        f.ViaId,
                        f.ParentId,
                        f.Created,
                        f.Status,
                        f.Loves,
                        f.Nsfw,
                        f.Sticky,
                        UserInfo = new
                        {
                            User = f.User.User1,
                            f.User.Name,
                            f.User.Lastname,
                            f.User.Avatar,
                            f.User.Background,
                            f.User.Cover,
                            f.User.Country,
                            f.User.Quote,
                            f.User.Rank,
                            f.User.Donation,
                            user_created = f.User.Created,
                            RankName = _context.Ranks.Where(r => f.User.Rank >= r.RequiredPoints).FirstOrDefault().Fullname,
                        }
                    })
                    .Where(f => f.Id == id).ToListAsync();

                return Ok(feedItem.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}
