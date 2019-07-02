using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FocLab.Consts;
using FocLab.Controllers.Base;
using FocLab.Extensions;
using FocLab.Logic.Extensions;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Services;
using FocLab.Logic.Workers.Users;
using FocLab.Model.Contexts;
using FocLab.Model.Enumerations;
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
        public UsersController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager) : base(context, userManager, signInManager)
        {
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
            ViewData["tuplesRoleAndRight"] = await UserSearcher.GetRightsAndRolesTupleAsync();
            ViewData["searchModel"] = model;

            var viewModel = await UserSearcher.SearchUsersAsync(model);

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
        /// Создание пользователя
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ViewBag.Rights = MvcExtensions.GetEnumDropdownList(typeof(UserRight));

            return View();
        }

        
        /// <summary>
        /// Редактирование пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var applicationUser = await UserSearcher.GetUserByIdAsync(id);

            if (applicationUser == null)
            {
                return RedirectToAction("Index");
            }
            //TODO Fix Logic
            var right = UserRoleWorker.GetHighRoleOfUser(await UserManager.GetRolesAsync(applicationUser.ToEntity()));
             
            var roles = new Dictionary<UserRight, bool>
            {
                {UserRight.SuperAdmin, false},
                {UserRight.Admin, false},
                {UserRight.Seller, false}
            };

            applicationUser.Rights.Intersect(roles.Keys).ToList().ForEach(x => roles[x] = true);
            ViewBag.UserRoles = roles.Skip(right);

            ViewData["sexes"] = MvcExtensions.GetSexesSelectList(applicationUser);

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
