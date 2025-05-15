using Core.Models;
using Core.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using QuickMock.Models.Request;

namespace QuickMock.Controllers
{
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
        public async Task<IActionResult> Requests()
        {
            var model = new RequestsModel
            {
                Requests = await _requestService.GetRequests()
            };
            return View("Requests", model);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> RequestValue(string path)
        {
            var requestValueResponse = await _requestService.GetRequestValue(path);
            return PartialView("RequestValue", new RequestEditModel
            {
                Path = path,
                Value = requestValueResponse.Value,
            });
        }

        [HttpGet("[action]")]
        public IActionResult RequestAdd()
        {
            return View("RequestAdd", new RequestAddModel());
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromForm] RequestAddModel request)
        {
            if (!ModelState.IsValid)
                return View("RequestAdd", request);
            
            await _requestService.AddRequest(request.Adapt<RequestModel>());
            return await Requests();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromForm] RequestEditModel request)
        {
            await _requestService.EditRequest(request.Adapt<RequestModel>());
            return Index();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(string path)
        {
            await _requestService.Delete(path);
            return Index();
        }
    }
}