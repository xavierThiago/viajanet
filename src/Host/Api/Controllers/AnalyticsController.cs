using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ViajaNet.JobApplication.Application.Core;
using ViajaNet.JobApplication.Application.Service;

namespace ViajaNet.JobApplication.Host.Api.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/analytics")]
    [Produces("application/json")]
    [AllowAnonymous]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsAppService _analyticsAppService;

        public AnalyticsController([FromServices] IAnalyticsAppService analyticsAppService)
        {
            if (analyticsAppService == null)
            {
                throw new InvalidOperationException($"Could not resolve {nameof(IAnalyticsAppService)} type from DI container.");
            }

            this._analyticsAppService = analyticsAppService;
        }

        /// <summary>
        /// Creates an analytics hit on server.
        /// </summary>
        /// <remarks>The information will be processed in a pub/sub structure.</remarks>
        /// <response code="201">Analytics hit created.</response>
        /// <response code="400">Invalid values were encoutered.</response>
        /// <response code="500">Oops! An unexpected error occurred. Analytics hit was not saved. Please, try again.</response>
        [HttpPost]
        [MapToApiVersion("1")]
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

        /// <summary>
        /// Retrieves an analytics information by ID.
        /// </summary>
        /// <remarks>All information are processed by CouchDB.</remarks>
        /// <response code="200">Analytics information was found.</response>
        /// <response code="400">Invalid values were encoutered.</response>
        /// <response code="500">Oops! An unexpected error occurred. Analytics hit was not saved. Please, try again.</response>
        [HttpGet("{id}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAnalyticsById([FromRoute] string id)
        {
            return await Task.Run(() =>
            {
                return new JsonResult(new SuccessResult<object>("Analytics hit found.", new { ip = "127.0.0.0" }));
            });
        }

        /// <summary>
        /// Retrieves an analytics information by IP or page name.
        /// </summary>
        /// <remarks>All information are processed by CouchDB.</remarks>
        /// <response code="200">Analytics information was found.</response>
        /// <response code="400">Invalid values were encoutered.</response>
        /// <response code="500">Oops! An unexpected error occurred. Analytics hit was not saved. Please, try again.</response>
        [HttpGet]
        [MapToApiVersion("1")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAnalyticsByQueryAsync([FromQuery] string ip = null, [FromQuery] string pageName = null)
        {
            return await Task.Run(() =>
            {
                return new JsonResult(new SuccessResult<object>("Analytics hit(s) found.", new { ip = "127.0.0.0" }));
            });
        }
    }
}
