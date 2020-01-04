using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
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
        [ProducesResponseType(typeof(SuccessResult<AnalyticsCreationResponsePayload>), 201)]
        [ProducesResponseType(typeof(ProblemDetails), 400)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [ProducesResponseType(typeof(ProblemDetails), 500)]
        public async Task<IActionResult> CreateAnalyticalHitAsync([FromBody] AnalyticsRequestPayload data)
        {
            var ipAddress = this.HttpContext.Connection.RemoteIpAddress;

            data.AddIp(ipAddress);

            string id = await this._analyticsAppService.CreateAsync(data.ToDto());

            return new CreatedResult
            (
                $"{this.HttpContext.Request.Path}?ip={ipAddress}",
                new SuccessResult<AnalyticsCreationResponsePayload>("Analytics hit created succesfully.", new AnalyticsCreationResponsePayload(id))
            );
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
        public async Task<IActionResult> GetAnalyticsById([FromRoute, StringLength(32)] string id)
        {
            if (!Guid.TryParse(id, out Guid guid))
            {
                this.ModelState.AddModelError("id", "Id format is incorrect.");

                return BadRequest(this.ModelState);
            }

            var result = await this._analyticsAppService.GetByIdAsync(id);

            return new JsonResult
            (
                new SuccessResult<AnalyticsResponsePayload>("Analytics hit found.", result.ToResponse())
            );
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
            if (ip != null && !IPAddress.TryParse(ip, out IPAddress ipAddress))
            {
                this.ModelState.AddModelError("ip", "IP format is incorrect.");
            }

            if (pageName != null && pageName == string.Empty)
            {
                this.ModelState.AddModelError("id", "Page name is empty.");

                return BadRequest(this.ModelState);
            }

            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var result = await this._analyticsAppService.GetByIPAndPageNameAsync(ip, pageName);
            var resultConverted = result.Select(x => x.ToResponse()).ToList();

            return new JsonResult
            (
                new SuccessResult<IEnumerable<AnalyticsResponsePayload>>
                (
                    "Analytics hit(s) found.", resultConverted
                )
            );
        }
    }
}
