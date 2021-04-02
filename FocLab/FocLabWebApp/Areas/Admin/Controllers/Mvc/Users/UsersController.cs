using System.Threading.Tasks;
using Clt.Contract.Models.Users;
using Clt.Logic.Services.Users;
using Croco.Core.Contract;
using FocLab.Consts;
using FocLab.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FocLab.Areas.Admin.Controllers.Mvc.Users
{
    /// <inheritdoc />
    /// <summary>
    /// Mvc-контроллер предоставляющий методы для работы с пользователями
    /// </summary>
    [Authorize(Roles = "Admin,SuperAdmin,Root")]
    [Area(AreaConsts.Admin)]
    public class UsersController : BaseController
    {
        UserSearcher UserSearcher { get; }

        public UsersController(UserSearcher userSearcher, 
            ICrocoRequestContextAccessor requestContextAccessor) : base(requestContextAccessor)
        {
            UserSearcher = userSearcher;
        }


        /// <summary>
        /// Список пользователей
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isPartial"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(UserSearch model, bool isPartial = false)
        {
            ViewData["searchModel"] = model;

            var viewModel = await UserSearcher.GetUsersAsync(model);

            return isPartial ? View("~/Areas/Admin/Views/Users/Partials/UsersList.cshtml", viewModel) : View(viewModel);
        }
        
        /// <summary>
        /// Подробнее о пользователе
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var applicationUser = await UserSearcher.GetUserByIdAsync(id);
            
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        /// <summary>
        /// Изменение пароля администратором
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ChangePassword(string id)
        {
            var applicationUser = await UserSearcher.GetUserByIdAsync(id);

            if(applicationUser == null)
            {
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }
    }
}
