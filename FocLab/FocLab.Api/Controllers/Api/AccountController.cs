using System.Threading.Tasks;
using Croco.Core.Contract.Models;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Workers.Account;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api
{
    public class SafeModel<T>
    {
        public string SafePass { get; set; }

        public T Payload { get; set; } 
    }

    /// <inheritdoc />
    /// <summary>
    /// Предоставляет методы для работы с учетной записью а также логгинирование
    /// </summary>
    [Route("Api/Account")]
    public class AccountController : Controller
    {
        private const string SafePass = "SomePass";
        private AccountManager AccountManager { get; }
        private AccountLoginWorker AccountLoginWorker { get; }

        /// <inheritdoc />
        public AccountController(AccountManager accountManager, 
            AccountLoginWorker accountLoginWorker)
        {
            AccountManager = accountManager;
            AccountLoginWorker = accountLoginWorker;
        }

        [HttpPost("Pass/Override")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> Login([FromBody] SafeModel<OverrideUserPasswordModel> model)
        {
            if(model.SafePass != SafePass)
            {
                return Task.FromResult(new BaseApiResponse(false, "Пароль не подхоит"));
            }

            return AccountManager.OverrideUserPassword(model.Payload);
        }


        #region Методы логинирования

        /// <summary>
        /// Войти по Email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login/ByEmail")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<LoginResultModel>))]
        public Task<BaseApiResponse<LoginResultModel>> Login([FromForm]LoginModel model)
        {
            return AccountLoginWorker.LoginAsync(model);
        }

        /// <summary>
        /// Войти по номеру телефона
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login/ByPhone")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<LoginResultModel>))]
        public Task<BaseApiResponse<LoginResultModel>> LoginByPhone([FromForm]LoginByPhoneNumberModel model)
        {
            return AccountLoginWorker.LoginByPhoneNumberAsync(model);
        }

        /// <summary>
        /// Войти под другим пользователем (доступно только исключительным пользователям)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login/AsUser")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> LoginAsUser([FromForm]UserIdModel model)
        {
            return AccountLoginWorker.LoginAsUserAsync(model);
        }
        
        #endregion

        #region Методы изменения
        /// <summary>
        /// Изменить пароль
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Change/Password")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> ChangePassword([FromForm]ChangeUserPasswordModel model)
        {
            return AccountManager.ChangePasswordAsync(model);
        }
        #endregion

        
        /// <summary>
        /// Проверить изменения пользователя (сравнивает залогиненного с тем же самым только в бд)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost("CheckUserChanges")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<ApplicationUserDto>))]
        public BaseApiResponse<ApplicationUserDto> CheckUserChanges()
        {
            return AccountManager.CheckUserChanges();
        }

        /// <summary>
        /// Разлогиниться в системе
        /// </summary>
        /// <returns></returns>
        [HttpPost("LogOut")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse))]
        public Task<BaseApiResponse> LogOut()
        {
            return AccountLoginWorker.LogOut(User);
        }
    }
}
