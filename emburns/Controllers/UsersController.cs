using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

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
            //RankName = _context.Ranks.Where(r => u.Rank >= r.RequiredPoints).FirstOrDefault().Fullname,

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


                // Ulgy way, I dont find a way to do this much more efficient AT the moment
                for (int i = 0; i < userList.Count; i++){
                    userList[i].RankName = _ranks
                        .Where(r => decimal.Truncate(r.RequiredPoints) == decimal.Truncate(userList[i].Rank))
                        .ToList().FirstOrDefault().Fullname;
                }


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

                userItem.RankName = _ranks
                        .Where(r => decimal.Truncate(r.RequiredPoints) == decimal.Truncate(userItem.Rank))
                        .ToList().FirstOrDefault().Fullname;

                return Ok(userItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }

}
