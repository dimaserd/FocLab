using FocLab.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;
using Tms.Logic.Models.Tasker;
using FocLab.Logic.Extensions;
using Microsoft.AspNetCore.Http;

namespace FocLab.Controllers.Mvc
{
    public class ScheduleController : BaseController
    {
        public ScheduleController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager, IHttpContextAccessor contextAccessor) : base(context, userManager, signInManager)
        {
        }

        /// <summary>
        /// Показать задания пользователей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResult Index(UserScheduleSearchModel model)
        {
            ViewData["searchModel"] = model;

            var viewModel = new CalendarMonthViewModel(model.MonthShift);

            return View(viewModel);
        }
    }
}