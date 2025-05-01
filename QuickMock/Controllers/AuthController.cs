using Core.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using QuickMock.Requests;

namespace QuickMock.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            await _authService.Register(request.Adapt<Core.Requests.RegistrationRequest>());
            return Ok();
        }
    }
}