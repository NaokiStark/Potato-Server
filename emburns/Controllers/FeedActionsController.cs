using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Enums;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace emburns.Controllers
{
    [Route("api/feed")]
    [ApiController]
    public class FeedActionsController : ControllerBase
    {
        private readonly mokyuContext _context;
        private IEnumerable<RankValue> _ranks;

        public FeedActionsController(mokyuContext context)
        {
            _context = context;

            _ranks = _context.Ranks.Select(r => new RankValue(r)).ToList(); // 
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(FeedSchemma feed)
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

                userItem.FetchUserRank(_ranks);


                FeedError feedValidation = feed.Validate();

                if (feedValidation != FeedError.None)
                {
                    throw new Exception($"Error Adding a new Feed: {feedValidation}");
                }

                int attachmttype = feed.AttachmentType ?? 0;
                bool nsfw = feed.Nsfw ?? false;

                var newFeed = new Feed()
                {
                    Userid = userItem.Id,
                    Text = feed.Text ?? "",
                    Attachment = feed.Attachment ?? "",
                    AttachmentType = attachmttype,
                    Nsfw = nsfw,
                    Sticky = false,
                    Status = false,
                    // default values
                    ViaId = 0,
                    ParentId = 0,
                    Wall = 0,
                    Loves = 0,
                };

                var resultEntity = _context.Feeds.Add(newFeed);


                _ = await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Shout agregado Correctamente",
                    item = new FeedBaseQuery(newFeed),
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}
