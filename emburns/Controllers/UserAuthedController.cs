using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Auth;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emburns.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserAuthedController : ControllerBase
    {
        private readonly mokyuContext _context;

        private IEnumerable<RankValue> _ranks;

        public UserAuthedController(mokyuContext context)
        {
            _context = context;
            _ranks = _context.Ranks.Select(r => new RankValue(r)).ToList(); // 
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin(AuthLogin userLogin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Bad Request" });
            }

            try
            {
                var userQuery = await _context.Users
                    .Where(u => u.User1.ToLower() == userLogin.User.ToLower())
                    .Select(u => new UserBaseQuery(u))
                    .ToListAsync();

                UserBaseQuery user = userQuery.FirstOrDefault();
                user.FetchUserRank(_ranks);

                if (user == null) {
                    throw new Exception("User not found");
                }

                string jwt = userLogin.GetToken(user);

                //return Ok(new { password = hashedPassword });
                return Ok(new { jwttoken = jwt });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}
