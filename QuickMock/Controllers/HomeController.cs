using Microsoft.AspNetCore.Mvc;

namespace QuickMock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Register()
        {
            return Ok();
        }
    }
}