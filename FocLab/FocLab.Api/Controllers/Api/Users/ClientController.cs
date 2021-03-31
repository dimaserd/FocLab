using System.Threading.Tasks;
using Clt.Contract.Models.Users;
using Clt.Logic.Services.Users;
using Croco.Core.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.Users
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер предоставляющий методы для работы c пользователем 
    /// </summary>
    [Route("Api/Client")]
    public class ClientController : Controller
    {
        private ClientWorker ClientWorker { get; }

        public ClientController(
            ClientWorker clientWorker)
        {
            ClientWorker = clientWorker;
        }


        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> Update(EditClient model)
        {
            return ClientWorker.EditClientAsync(model);
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
        [ProducesDefaultResponseType(typeof(BaseApiResponse<ClientModel>))]
        public Task<BaseApiResponse<ClientModel>> Get()
        {
            return ClientWorker.GetClientFromAuthorizationAsync();
        }
    }
}