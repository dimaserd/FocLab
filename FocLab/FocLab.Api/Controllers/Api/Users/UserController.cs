using System.Threading.Tasks;
using Croco.Core.Common.Models;
using Croco.Core.Search;
using Croco.Core.Search.Models;
using FocLab.Api.Controllers.Base;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Models;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.Account;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api.Users
{
    /// <inheritdoc />
    /// <summary>
    /// Предоставляет методы для работы с пользователями
    /// </summary>s
    [Route("Api/User")]
    public class UserController : BaseApiController
    {
        /// <inheritdoc />
        public UserController(ChemistryDbContext context, ApplicationSignInManager signInManager, ApplicationUserManager userManager, IHttpContextAccessor httpContextAccessor) : base(context, signInManager, userManager, httpContextAccessor)
        {
        }

        private UserSearcher UserSearcher => new UserSearcher(ContextWrapper);

        private UserWorker UserWorker => new UserWorker(ContextWrapper);

        private AccountRegistrationWorker AccountRegistrationWorker => new AccountRegistrationWorker(ContextWrapper);

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
            return AccountRegistrationWorker.RegisterUserByAdminAsync(model, UserManager, model.Rights);
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
            return UserWorker.ChangePasswordAsync(model, UserManager);
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
