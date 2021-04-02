using System.Threading.Tasks;
using Clt.Contract.Models.Common;
using Clt.Contract.Models.Users;
using Clt.Logic.Services.Users;
using Croco.Core.Contract.Models;
using Croco.Core.Contract.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.Users
{
    /// <inheritdoc />
    /// <summary>
    /// Предоставляет методы для работы с пользователями
    /// </summary>s
    [Route("Api/User")]
    public class UserController : Controller
    {
        private UserSearcher UserSearcher { get; }

        private UserWorker UserWorker { get; }
        
        /// <inheritdoc />
        public UserController(
            UserSearcher userSearcher,
            UserWorker userWorker)
        {
            UserSearcher = userSearcher;
            UserWorker = userWorker;
        }


        /// <summary>
        /// Получает список всех пользователей
        /// </summary>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(GetListResult<ApplicationUserBaseModel>))]
        [HttpPost("Get")]
        public Task<GetListResult<ApplicationUserBaseModel>> GetUsers(UserSearch model)
        {
            return UserSearcher.GetUsersAsync(model);
        }

        /// <summary>
        /// Метод  поиска пользователя по номеру телефона
        /// </summary>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(ApplicationUserBaseModel))]
        [HttpPost("Search/ByPhone")]
        public Task<ApplicationUserBaseModel> SearchByPhone([FromQuery]string phone)
        {
            return UserSearcher.GetUserByPhoneNumberAsync(phone);
        }

        /// <summary>
        /// Метод поиска пользователя по адресу электронной почты
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(ApplicationUserBaseModel))]
        [HttpPost("Search/ByEmail")]
        public Task<ApplicationUserBaseModel> SearchByEmail([FromQuery]string email)
        {
            return UserSearcher.GetUserByEmailAsync(email);
        }

        /// <summary>
        /// Активирование или деактивирование пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("ActivateOrDeActivateUser")]
        public Task<BaseApiResponse> ActivateOrDeActivateUser(UserActivation model)
        {
            return UserWorker.ActivateOrDeActivateUserAsync(model);
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("Remove")]
        public Task<BaseApiResponse> Remove(UserIdModel model)
        {
            return UserWorker.RemoveUserAsync(model.Id);
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("Edit")]
        public Task<BaseApiResponse> Edit(EditApplicationUser model)
        {
            return UserWorker.EditUserAsync(model);
        }
    }
}
