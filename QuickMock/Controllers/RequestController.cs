using Core.Models;
using Core.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using QuickMock.Models.Request;

namespace QuickMock.Controllers
{
    [Route("")]
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
        public async Task<IActionResult> RequestValue(string url)
        {
            var requestValueResponse = await _requestService.GetRequestValue(url);
            return PartialView("_RequestValue", new RequestEditModel
            {
                Url = url,
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
            return PartialView("_RequestValue", request);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete(string path)
        {
            await _requestService.Delete(path);
            return Index();
        }
    }
}