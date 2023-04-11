using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Enums;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace emburns.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class FeedCommentController : ControllerBase
    {

        private readonly mokyuContext _context;
        private IEnumerable<RankValue> _ranks;

        public FeedCommentController(mokyuContext context)
        {
            _context = context;

            _ranks = _context.Ranks.Select(r => new RankValue(r)).ToList(); // 
        }

        // GET api/comment/5        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return NotFound("Not implemented: api/comment/{id}");
        }

        /**
         * I Used FeedSchemma to save time, ToDo: create a new input type
         */
        // POST api/comment
        [HttpPost]
        public async Task<IActionResult> Post(CommentSchemma feed)
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


                FeedError feedValidation = await feed.Validate(_context);

                if (feedValidation != FeedError.None)
                {
                    throw new Exception($"Error Adding a new Comment: {feedValidation}");
                }

                int attachmttype = feed.AttachmentType ?? 0;
                bool nsfw = feed.Nsfw ?? false;

                var newComment = new Comment()
                {
                    Postid = feed.Parent,
                    Text = feed.Text,
                    Status = false,
                    Userid = userItem.Id,
                };

                var resultEntity = _context.Comments.Add(newComment);


                _ = await _context.SaveChangesAsync();

                return Ok(new { message = "Comentario agregado Correctamente",
                    id = $"{newComment.Id}" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}
