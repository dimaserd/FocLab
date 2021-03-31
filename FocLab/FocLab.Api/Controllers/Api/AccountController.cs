using System.Threading.Tasks;
using Clt.Contract.Models.Account;
using Clt.Contract.Models.Common;
using Clt.Logic.Models.Account;
using Clt.Logic.Services.Account;
using Croco.Core.Contract.Models;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Api.Controllers.Api
{
    /// <inheritdoc />
    /// <summary>
    /// Предоставляет методы для работы с учетной записью а также логгинирование
    /// </summary>
    [Route("Api/Account")]
    public class AccountController : Controller
    {
        private AccountInitiator AccountManager { get; }
        private AccountLoginWorker AccountLoginWorker { get; }

        /// <inheritdoc />
        public AccountController(AccountInitiator accountManager, 
            AccountLoginWorker accountLoginWorker)
        {
            AccountManager = accountManager;
            AccountLoginWorker = accountLoginWorker;
        }

        
        #region Методы логинирования

        /// <summary>
        /// Войти по Email
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("Login/ByEmail")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<LoginResultModel>))]
        public Task<BaseApiResponse<LoginResultModel>> Login([FromBody]LoginModel model)
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
        public Task<BaseApiResponse<LoginResultModel>> LoginByPhone([FromBody] LoginByPhoneNumberModel model)
        {
            return AccountLoginWorker.LoginByPhoneNumberAsync(model);
        }

        #endregion

        #region Методы изменения
     
        #endregion

        
        /// <summary>
        /// Проверить изменения пользователя (сравнивает залогиненного с тем же самым только в бд)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost("CheckUserChanges")]
        [ProducesDefaultResponseType(typeof(BaseApiResponse<ApplicationUserBaseModel>))]
        public BaseApiResponse<ApplicationUserBaseModel> CheckUserChanges()
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
            return AccountLoginWorker.LogOut();
        }
    }
}
