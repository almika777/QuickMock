using Microsoft.AspNetCore.Mvc;

namespace QuickMock.Controllers
{
    [ApiController]
    [Route("Api/{*path}")]
    public class ApiController : ControllerBase
    {

        [HttpGet]
        [HttpPost] 
        public IActionResult HandleRequest([FromRoute] string path, [FromQuery] Dictionary<string, string> queryParams)
        {
            // Полный путь (например, "qwe/?text=1")
            string fullPathWithQuery = $"{path}{Request.QueryString}";

            // Логирование или обработка
            Console.WriteLine($"Full path: {fullPathWithQuery}");
            Console.WriteLine($"Query params: {string.Join(", ", queryParams)}");

            return Ok(new
            {
                Path = path,
                Query = queryParams,
                FullUrl = $"/Api/{fullPathWithQuery}"
            });
        }
    }
}