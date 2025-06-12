using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace QuickMock.Controllers
{
    [ApiController]
    public class ApiController(IRequestProviderService requestProvider) : ControllerBase
    {
        [Route("api/{*path}")]
        [HttpGet, HttpPost]
        public async Task<IActionResult> HandleRequest([FromRoute] string path)
        {
            var request = await requestProvider.GetRequestValue(path);
            return request.Value != null
                ? Ok(request.Value)
                : BadRequest("Request was not found");
        }
    }
}