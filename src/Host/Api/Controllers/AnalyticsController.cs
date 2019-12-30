using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ViajaNet.JobApplication.Host.Api.Controllers
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
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateAnalyticalHitAsync([FromBody] AnalyticsPayload data)
        {
            var ipAddress = this.HttpContext.Connection.RemoteIpAddress;

            data.AddIp(ipAddress);

            return await Task.Run(() =>
            {
                return new CreatedResult($"{this.HttpContext.Request.Path}?ip={ipAddress}",
                                            new SuccessResult<AnalyticsPayload>("Analytics hit created succesfully.", data));
            });
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAnalyticsByQueryAsync([FromQuery] string ip = null, [FromQuery] string pageName = null)
        {
            return await Task.Run(() =>
            {
                return new JsonResult(new SuccessResult<object>("Analytics hit created succesfully.", new { ip = "127.0.0.0" }));
            });
        }
    }
}
