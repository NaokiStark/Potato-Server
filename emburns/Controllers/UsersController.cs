using emburns.Models;
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

        public UsersController(mokyuContext context)
        {
            _context = context;
        }

        

        [HttpGet("name")]
        public async Task<IActionResult> GetByUsername(string username)
        {
            try
            {
                var userList = await _context.Users
                    .Select(u => new
                    {
                        u.Id,
                        User = u.User1,
                        u.Name,
                        u.Lastname,
                        u.Avatar,
                        u.Background,
                        u.Cover,
                        u.Country,
                        u.Quote,
                        u.Rank,
                        u.Donation,
                        u.Created,
                        RankName = _context.Ranks.Where(r => u.Rank >= r.RequiredPoints).FirstOrDefault().Fullname,

                    })
                    .OrderBy(u => u.Id)
                    .Where(u => u.Name == username)
                    .ToListAsync();

                var userItem = userList.FirstOrDefault();

                if (userItem == null)
                {
                    return NotFound(new { message = "Not Found" });
                }

                return Ok(userItem);
            }
            catch(Exception ex)
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
                    .Select(u => new
                    {
                        u.Id,
                        User = u.User1,
                        u.Name,
                        u.Lastname,
                        u.Avatar,
                        u.Background,
                        u.Cover,
                        u.Country,
                        u.Quote,
                        u.Rank,
                        u.Donation,
                        u.Created,
                        RankName = _context.Ranks.Where(r => u.Rank >= r.RequiredPoints).FirstOrDefault().Fullname,

                    })
                    .OrderBy(u => u.Id)
                    .Where(u => u.Id == id)
                    .ToListAsync();

                var userItem = userList.FirstOrDefault();

                if (userItem == null)
                {
                    return NotFound(new { message = "Not Found" });
                }

                return Ok(userItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }

}
