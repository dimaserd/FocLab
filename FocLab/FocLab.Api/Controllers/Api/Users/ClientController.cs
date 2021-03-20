using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Workers.Users;
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
        private ClientWorker ClientWorker { get; }

        public ClientController(ICrocoRequestContextAccessor requestContextAccessor,
            ClientWorker clientWorker) : base(requestContextAccessor)
        {
            ClientWorker = clientWorker;
        }


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