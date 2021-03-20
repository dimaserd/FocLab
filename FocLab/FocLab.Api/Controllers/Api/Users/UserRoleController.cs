using System.Threading.Tasks;
using Croco.Core.Contract.Models;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Workers.Users;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.Users
{
    /// <inheritdoc />
    /// <summary>
    /// Контроллер предоставляющий методы для работы c ролями пользователей 
    /// </summary>
    [Route("Api/User/Role")]
    public class UserRoleController: Controller
    {
        private UserRoleWorker UserRoleWorker { get; }

        /// <inheritdoc />
        public UserRoleController(UserRoleWorker userRoleWorker)
        {
            UserRoleWorker = userRoleWorker;
        }

        /// <summary>
        /// Добавление роли
        /// </summary>
        [HttpPost(nameof(Add))]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> Add([FromForm]UserIdAndRole model)
        {
            return UserRoleWorker.AddUserToRoleAsync(model);
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        [HttpPost(nameof(Remove))]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> Remove([FromForm]UserIdAndRole model)
        {
            return UserRoleWorker.RemoveRoleFromUserAsync(model);
        }
    }
}
