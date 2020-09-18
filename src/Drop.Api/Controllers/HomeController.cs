using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Drop.Api.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        private readonly IOptions<ApiOptions> _apiOptions;

        public HomeController(IOptions<ApiOptions> apiOptions)
        {
            _apiOptions = apiOptions;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(_apiOptions.Value.Name);
        }
    }
}