using Core.Requests;
using Core.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using QuickMock.Requests;

namespace QuickMock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController(IRequestProviderService requestService) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AddRequest request)
        {
            await requestService.AddRequest(request.Adapt<RequestAddRequest>());
            return Ok();
        }
    }
}