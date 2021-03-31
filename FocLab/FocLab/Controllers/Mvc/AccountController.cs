using System.Threading.Tasks;
using Clt.Logic.Services.Account;
using Clt.Logic.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>

    [Authorize]
    public class AccountController : Controller
    {
        AccountLoginWorker AccountLoginWorker { get; }
        UserSearcher UserSearcher { get; }
        UserWorker UserWorker { get; }

        public AccountController(AccountLoginWorker accountLoginWorker,
            UserSearcher userSearcher,
            UserWorker userWorker)
        {
            AccountLoginWorker = accountLoginWorker;
            UserSearcher = userSearcher;
            UserWorker = userWorker;
        }

        


        #region Обычные методы
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if(returnUrl != null && returnUrl.ToLower() == "/account/login")
            {
                returnUrl = null;
            }

            ViewBag.ReturnUrl = returnUrl;

            if(!User.Identity.IsAuthenticated)
            {
                return View("~/Metronic5/Login.cshtml");
            }
            
            return Redirect("~/");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        
        /// <summary>
        /// Сбросить пароль
        /// </summary>
        /// <param name="code"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(string code, string userId)
        {
            var user = await UserSearcher.GetUserByIdAsync(userId);

            ViewData["user"] = user;

            return code == null ? View("Error") : View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        
        /// <summary>
        /// Разлогинить в системе
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {   
            await AccountLoginWorker.LogOut();
            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// Разлогинить в системе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await AccountLoginWorker.LogOut();
            return RedirectToAction("Index", "Home");
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult ExternalLoginFailure()
        {
            return View();
        }

        #endregion

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}