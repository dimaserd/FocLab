using Croco.Core.Contract.Models;
using Microsoft.AspNetCore.Mvc;
using MigrationTool.Tools;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api.FocLab
{
    [Route("Api/Migration")]
    public class MigrationApiController : Controller
    {
        AddSnaphotsForEntities AddSnaphotsForEntities { get; }

        AddDbFileHistory AddDbFileHistory { get; }

        public MigrationApiController(AddSnaphotsForEntities addSnaphotsForEntities,
            AddDbFileHistory addDbFileHistory)
        {
            AddSnaphotsForEntities = addSnaphotsForEntities;
            AddDbFileHistory = addDbFileHistory;
        }
        
        [HttpPost("MakeSnapshots")]
        public Task<BaseApiResponse> MakeSnapshots()
        {
            return AddSnaphotsForEntities.Execute();
        }

        [HttpPost("CheckFileHistoryCount")]
        [ProducesDefaultResponseType(typeof(int))]
        public Task<int> CheckFileHistoryCount()
        {
            return AddDbFileHistory.GetCountLeftAsync();
        }

        [HttpPost("AddFileHistory")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> AddFileHistory()
        {
            return AddDbFileHistory.Execute();
        }
    }
}