using Croco.Core.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MigrationTool.Tools;
using System.Threading.Tasks;

namespace FocLab.Api.Controllers.Api.FocLab
{
    [Route("Api/Migration")]
    public class MigrationApiController : BaseApiController
    {
        public MigrationApiController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        public AddSnaphotsForEntities AddSnaphotsForEntities => new AddSnaphotsForEntities(AmbientContext);

        public AddDbFileHistory AddDbFileHistory => new AddDbFileHistory(AmbientContext);

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
