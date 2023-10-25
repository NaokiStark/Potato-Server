using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using emburns.Models;
using emburns.PotatoModels;
using emburns.PotatoModels.Extras;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace emburns.Controllers
{
    [Route("api/community")]
    [ApiController]
    public class CommunitiesController : ControllerBase
    {
        private readonly mokyuContext _context;
        private IEnumerable<RankValue> _ranks;


        public CommunitiesController(mokyuContext context)
        {
            _context = context;
            _ranks = _context.Ranks.Select(r => new RankValue(r)).ToList(); // 

        }

        // GET: api/community
        [HttpGet]
        public async Task<IActionResult> GetCommunities(int page = 0, int limit = 10)
        {
            try
            {

                var communities = await _context.Communities
                    .Include(c => c.CreatorNavigation)
                    .Include(c => c.Category)
                    .Include(c => c.CommunitiesMembers)
                    .ThenInclude(u => u.User)

                    .OrderByDescending(c => c.Id)
                    .Where(c => c.Status == 0)
                    .Skip(page * limit)
                    .Take(limit)
                    .Select(c => new CommunityQueryBase(c, false))
                    .ToListAsync();

                return Ok(communities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }

        }

        // GET: api/community/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommunity(int id, bool withMembers = false)
        {
            try
            {
                var communities = await _context.Communities
                        .Include(c => c.CreatorNavigation)
                        .Include(c => c.Category)
                        .Include(c => c.CommunitiesMembers)
                        .ThenInclude(u => u.User)

                        .OrderByDescending(c => c.Id)
                        .Where(c => c.Status == 0)
                        .Where(c => c.Id == id)
                        .Select(c => new CommunityQueryBase(c, withMembers))
                        .ToListAsync()
                        ;

                var result = communities.FirstOrDefault();

                if (result == null)
                {
                    return NotFound(new { message = "Recurso no encontrado" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }

        [HttpGet("posts")]
        public async Task<IActionResult> GetPosts(int community = 0, int page = 0, int limit = 10)
        {
            try
            {
                List<PostBaseQuery> posts;
                if (community == 0)
                {
                    posts = await _context.Posts
                    // category
                    .Include(p => p.Community)
                    .ThenInclude(c => c.Category)
                    // community Creator
                    .Include(p => p.Community)
                    .ThenInclude(c => c.CreatorNavigation)
                    //post creator
                    .Include(p => p.CreatorNavigation)

                    .OrderByDescending(p => p.Id)
                    .Where(p => p.Status == 0)
                    .Skip(page * limit)
                    .Take(limit)
                    .Select(p => new PostBaseQuery(p, false))
                    .ToListAsync();
                }
                else
                {
                    posts = await _context.Posts
                    // category
                    .Include(p => p.Community)
                    .ThenInclude(c => c.Category)
                    // community Creator
                    .Include(p => p.Community)
                    .ThenInclude(c => c.CreatorNavigation)
                    //post creator
                    .Include(p => p.CreatorNavigation)

                    .OrderByDescending(p => p.Id)
                    .Where(p => p.Status == 0 && p.CommunityId == community)
                    .Skip(page * limit)
                    .Take(limit)
                    .Select(p => new PostBaseQuery(p, false))
                    .ToListAsync();
                }

                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }

        [HttpGet("posts/{id}")]
        public async Task<IActionResult> GetPost(int id = 0)
        {
            try
            {
                var posts = await _context.Posts
                    // category
                    .Include(p => p.Community)
                    .ThenInclude(c => c.Category)
                    // community Creator
                    .Include(p => p.Community)
                    .ThenInclude(c => c.CreatorNavigation)
                    //post creator
                    .Include(p => p.CreatorNavigation)

                    .OrderByDescending(p => p.Id)
                    .Where(p => p.Status == 0 && p.Id == id)
                    .Select(p => new PostBaseQuery(p, true))
                    .ToListAsync();

                var result = posts.FirstOrDefault();

                if(result == null)
                {
                    return NotFound(new { message = "Recurso no encontrado" });
                }

                result.User.FetchUserRank(_ranks);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }
}
