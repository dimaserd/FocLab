using System.Threading.Tasks;
using Croco.Core.Contract.Models;
using Croco.Core.Contract.Models.Search;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Workers.Account;
using FocLab.Logic.Workers.Users;
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
        private AccountRegistrationWorker AccountRegistrationWorker { get; }


        /// <inheritdoc />
        public UserController(
            UserSearcher userSearcher,
            UserWorker userWorker,
            AccountRegistrationWorker accountRegistrationWorker)
        {
            UserSearcher = userSearcher;
            UserWorker = userWorker;
            AccountRegistrationWorker = accountRegistrationWorker;
        }


        
        /// <summary>
        /// Получает список всех пользователей
        /// </summary>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(GetListResult<ApplicationUserBaseModel>))]
        [HttpPost("Get")]
        public Task<GetListResult<ApplicationUserBaseModel>> GetUsers([FromForm]UserSearch model)
        {
            return UserSearcher.GetUsersAsync(model);
        }

        /// <summary>
        /// Метод  поиска пользователя по номеру телефона
        /// </summary>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        [HttpPost("Search/ByPhone")]
        public Task<ApplicationUserDto> SearchByPhone([FromQuery]string phone)
        {
            return UserSearcher.GetUserByPhoneNumberAsync(phone);
        }

        /// <summary>
        /// Метод поиска пользователя по адресу электронной почты
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(ApplicationUserDto))]
        [HttpPost("Search/ByEmail")]
        public Task<ApplicationUserDto> SearchByEmail([FromQuery]string email)
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
        public Task<BaseApiResponse> ActivateOrDeActivateUser([FromForm]UserActivation model)
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
        public Task<BaseApiResponse> Remove([FromForm]UserIdModel model)
        {
            return UserWorker.RemoveUserAsync(model);
        }

        /// <summary>
        /// Изменение пользователя администратором
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse<ApplicationUserDto>))]
        [HttpPost("Create")]
        public Task<BaseApiResponse<ApplicationUserDto>> Create([FromForm]CreateUserModel model)
        {
            return AccountRegistrationWorker.RegisterUserByAdminAsync(model, model.Rights);
        }

        /// <summary>
        /// Изменение пользователя администратором
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("ChangePassword")]
        public Task<BaseApiResponse> ChangePassword([FromForm]ResetPasswordByAdminModel model)
        {
            return UserWorker.ChangePasswordAsync(model);
        }

        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        [HttpPost("Edit")]
        public Task<BaseApiResponse> Edit([FromForm]EditApplicationUser model)
        {
            return UserWorker.EditUserAsync(model);
        }
    }
}
