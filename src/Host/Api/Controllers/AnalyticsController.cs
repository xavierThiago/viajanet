using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ViajaNet.JobApplication.Host.Web.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/analytics")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class AnalyticsController : ControllerBase
    {
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAnalyticalHitAsync([FromBody] object data)
        {
            return await Task.Run(() => new JsonResult(new { status = true }));
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAnalyticsByQueryAsync([FromQuery] string ip = null, [FromQuery] string page_name = null)
        {
            return await Task.Run(() =>  new JsonResult(new { status = true }));
        }
    }
}
