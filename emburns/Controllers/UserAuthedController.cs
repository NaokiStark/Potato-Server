using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Auth;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Authorization;
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
                    .Select(u => new { data = new UserBaseQuery(u), u.Password })
                    .ToListAsync();

                var user = userQuery.FirstOrDefault();

                if (user == null) {
                    throw new Exception("User not found");
                }

                user.data.FetchUserRank(_ranks);

                var loginCheck = userLogin.VerifyHashedPassword(user.Password, userLogin.Password);

                if (!loginCheck)
                {
                    throw new Exception("Usuario y/o contraseña incorrectos.");
                }

                string jwt = userLogin.GetToken(user.data);

                //return Ok(new { password = hashedPassword });
                return Ok(new {userData = user.data, token = jwt });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }

        [Authorize]
        [HttpGet("testlogin")]
        public IActionResult TestAuthToken()
        {
            return Ok("Token passed");
        }

        [Authorize]
        [HttpGet("getLogin")]
        public async Task<IActionResult> GetUserLoginData()
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

                return Ok(userItem);
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}
