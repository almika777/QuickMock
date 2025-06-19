using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace QuickMock.Controllers
{
    [ApiController]
    public class ApiController(IRequestProviderService requestProvider) : ControllerBase
    {
        [Route("api/{**path}")]
        [HttpGet, HttpPost]
        public async Task<IActionResult> HandleRequest(string path)
        {
            var fullPath = $"{path}{HttpContext.Request.QueryString}";
            var request = await requestProvider.GetRequestValue(fullPath);
            return request.Value != null
                ? Ok(request.Value)
                : BadRequest("Request was not found");
        }
    }
}