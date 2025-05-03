using Core.Services;
using Microsoft.AspNetCore.Mvc;
using QuickMock.Requests;

namespace QuickMock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController(
        IRequestService requestService) : ControllerBase
    {
        [HttpPost]
        public IActionResult Add(AddRequest request)
        {
            return Ok();
        }
    }
}