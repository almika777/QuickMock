using Core.Requests;
using Core.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using QuickMock.Models.Request;

namespace QuickMock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController : Controller
    {
        private readonly IRequestProviderService _requestService;
        public RequestController(IRequestProviderService requestService)
        {
            _requestService = requestService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> RequestsTab()
        {
            var model = new RequestIndexModel
            {
                Requests = await _requestService.GetRequests()
            };
            return PartialView("RequestsTab", model);
        }        
        
        [HttpGet("[action]")]
        public IActionResult RequestAddTab()
        {
            return PartialView("RequestAddTab", new RequestAddModel());
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromForm]RequestAddModel request)
        {
            await _requestService.AddRequest(request.Adapt<RequestAddRequest>());
            return Index();
        }
    }
}