using Microsoft.AspNetCore.Mvc;

namespace SportCompetitionsAPI.Controllers.Controllers
{
    [ApiController]
    [Route("api/home")]
    public class HomeController : ControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok("Привет мир");
        }
    }
}
