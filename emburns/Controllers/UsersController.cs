using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emburns.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly mokyuContext _context;

        private IEnumerable<RankValue> _ranks;
        public UsersController(mokyuContext context)
        {
            _context = context;
            _ranks = _context.Ranks.Select(r => new RankValue(r)).ToList(); // 
        }


        [HttpGet("name")]
        public async Task<IActionResult> GetByUsername(string username, bool searchMode = false)
        {
            try
            {
                List<UserBaseQuery> userList;

                if (searchMode)
                {
                    userList = await _context.Users
                    .OrderBy(u => u.Id)
                    .Where(u => u.User1.Contains(username))
                    .Select(u => new UserBaseQuery(u))
                    .Take(10)
                    .ToListAsync();
                }
                else
                {
                    userList = await _context.Users
                    .OrderBy(u => u.Id)
                    .Where(u => u.User1 == username)
                    .Select(u => new UserBaseQuery(u))
                    .Take(10)
                    .ToListAsync();
                }

                userList.ForEach(u => u.FetchUserRank(_ranks));

                return Ok(userList);
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

                var userList = await _context.Users
                    .OrderBy(u => u.Id)
                    .Where(u => u.Id == id)
                    .Select(u => new UserBaseQuery(u))
                    .ToListAsync();

                var userItem = userList.FirstOrDefault();

                if (userItem == null)
                {
                    return NotFound(new { message = "Not Found" });
                }

                userItem.FetchUserRank(_ranks);

                return Ok(userItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }

}
