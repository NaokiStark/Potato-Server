using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emburns.Controllers
{
    [Route("api/reaction")]
    [ApiController]
    public class ReactionController : Controller
    {
        private readonly mokyuContext _context;
        private IEnumerable<RankValue> _ranks;

        public ReactionController(mokyuContext context)
        {
            _context = context;
            _ranks = _context.Ranks.Select(r => new RankValue(r)).ToList();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> addReaction(ReactionRequest reaction)
        {
            try
            {
                string username = User.Identity.Name;

                var usersList = await _context.Users
                        .OrderBy(u => u.Id)
                        .Where(u => u.User1 == username)
                        .Select(u => new UserBaseQuery(u))
                        .Take(10)
                        .ToListAsync();

                var userItem = usersList.FirstOrDefault();

                if (userItem == null)
                {
                    throw new Exception("Bad Request");
                }

                var feedItems = await _context.Feeds.AsNoTracking()
                    .Include(f => f.User)
                    .Include(f => f.Via)

                    .Include(f => f.Comments)
                    .ThenInclude(f => f.User)

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.User)

                    .Include(f => f.LovesNavigation)
                    .ThenInclude(l => l.ReactionNavigation)

                    .Where(f => f.Id == reaction.feedId)
                    .Where(f => f.Status == false)
                    .Select(f => new FeedBaseQuery(f))
                    .ToListAsync();

                var feedItem = feedItems.FirstOrDefault();

                // Feed not found
                if (feedItem == null)
                {
                    return BadRequest(new { message = "El Shout no existe, INTERNAUTA!" });
                }

                var reactions = await _context.ReactionsLists
                    .Where(r => r.Id == reaction.reactionId)
                    .ToListAsync();

                var selectedReaction = reactions.FirstOrDefault();

                // Invalid reaction
                if (selectedReaction == null)
                {
                    return BadRequest(new { message = "Ya te vi, la proxima te baneo. Digo, la reacción no existe." });
                }

                if (feedItem.LovesList.Any(x => x.User.Username == username && x.Reaction.ReactionText == selectedReaction.ReactionText))
                {
                    return BadRequest(new { message = "Ya votaste este Shout" });
                }

                Lofe newReaction = new()
                {
                    Userid = userItem.Id,
                    Post = reaction.feedId,
                    Reaction = reaction.reactionId,
                };

                var resultEntity = _context.Loves.Add(newReaction);

                _ = await _context.SaveChangesAsync();

                return Ok(new ReactionQuery(selectedReaction));
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}