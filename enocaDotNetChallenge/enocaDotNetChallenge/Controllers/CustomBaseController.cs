using enocaDotNetChallenge.Core.DTO_s;
using enocaDotNetChallenge.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace enocaDotNetChallenge.Controllers
{
    [ValidateFilterAttribute]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(CustomResponseDTO<T> response)
        {

            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
