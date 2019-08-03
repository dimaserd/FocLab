using System.Threading.Tasks;
using Croco.Core.Common.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.Users
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер предоставляющий методы для работы c пользователем 
    /// </summary>
    [Route("Api/Client")]
    public class ClientController : BaseApiController
    {
        /// <inheritdoc />
        public ClientController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }



        private ClientWorker ClientWorker => new ClientWorker(ContextWrapper, x => SignInManager.SignInAsync(x, true));

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost(nameof(Update))]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> Update([FromForm]EditClient model)
        {
            return ClientWorker.EditUserAsync(model);
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <returns></returns>
        [HttpPost(nameof(UpdateClientPhoto))]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> UpdateClientPhoto(int fileId)
        {
            return ClientWorker.UpdateClientPhotoAsync(fileId);
        }

        /// <summary>
        /// Получение пользователя
        /// </summary>
        [HttpGet(nameof(Get))]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse<ClientModel>> Get()
        {
            return ClientWorker.GetUserAsync();
        }
    }
}
