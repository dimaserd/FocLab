using System;
using System.Threading.Tasks;
using Croco.Core.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api
{
    public class FrontExceptionData
    {
        public DateTime ExceptionDate { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public string Uri { get; set; }
    }

    public class FrontLogData
    {
        public DateTime LogDate { get; set; }
        public string EventId { get; set; }
        public string ParametersJson { get; set; }
        public string Uri { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
    }

    [Route("Api/Log")]
    public class LogController : Controller
    {
        [HttpPost("Exception")]
        public Task<BaseApiResponse> LogException([FromBody] FrontExceptionData model)
        {
            return Task.FromResult(new BaseApiResponse(true, ""));
        }

        [HttpPost("Action")]
        public Task<BaseApiResponse> LogAction([FromBody] FrontLogData model)
        {
            return Task.FromResult(new BaseApiResponse(true, ""));
        }
    }
}