using System.Threading.Tasks;
using Croco.Core.Contract;
using Croco.Core.Contract.Models;
using FocLab.Controllers.Base;
using FocLab.Logic.EntityDtos.Users.Default;
using FocLab.Logic.Models.Account;
using FocLab.Logic.Workers.Account;
using FocLab.Logic.Workers.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Controllers.Mvc
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>

    [Authorize]
    public class AccountController : BaseController
    {
        AccountLoginWorker AccountLoginWorker { get; }
        AccountManager AccountManager { get; }
        UserSearcher UserSearcher { get; }
        UserWorker UserWorker { get; }

        public AccountController(AccountLoginWorker accountLoginWorker,
            AccountManager accountManager,
            UserSearcher userSearcher,
            ICrocoRequestContextAccessor requestContextAccessor,
            UserWorker userWorker) : base(requestContextAccessor)
        {
            AccountLoginWorker = accountLoginWorker;
            AccountManager = accountManager;
            UserSearcher = userSearcher;
            UserWorker = userWorker;
        }

        
        #region Dev Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task<string> Dev()
        {
            var result = await AccountManager.InitAsync();

            return result.Message;
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> ResetPass(ApplicationUserDto model)
        {
            var user = await UserSearcher.GetUserByIdAsync(model.Id);

            if(user == null)
            {
                return Json(new BaseApiResponse(false, "Произошла ошибка"));
            }

            if (user.ObjectJson != model.ObjectJson)
            {
                return Json(new BaseApiResponse(false, "Произошла ошибка"));
            }

            var t = await UserWorker.ChangePasswordBaseAsync(new ResetPasswordByAdminModel
            {
                Email = user.Email,
                Password = user.PasswordHash
            });

            if(!t.IsSucceeded)
            {
                return Json(new BaseApiResponse(false, "Произошла ошибка"));
            }

            return Json(new BaseApiResponse(true, "Ваш пароль изменен"));
        }

        #region Обычные методы
        
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

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
            await AccountLoginWorker.LogOut(User);
            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// Разлогинить в системе
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await AccountLoginWorker.LogOut(User);
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