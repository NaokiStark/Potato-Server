using emburns.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace emburns.Controllers
{
    [Route("api/site_config")]
    [ApiController]
    public class SiteConfigController : ControllerBase
    {
        private readonly mokyuContext _context;
        
        public SiteConfigController(mokyuContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {

                var reactns = await _context.ReactionsLists.ToListAsync();

                return Ok(new
                {
                    reactions = reactns
                });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = $"{ex.Message}\n{ex.StackTrace}" });
            }
        }
    }


}
