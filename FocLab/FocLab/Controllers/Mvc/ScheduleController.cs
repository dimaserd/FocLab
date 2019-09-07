using FocLab.Controllers.Base;
using FocLab.Logic.Services;
using FocLab.Model.Contexts;
using Microsoft.AspNetCore.Mvc;
using Tms.Logic.Models.Tasker;
using Microsoft.AspNetCore.Http;
using Tms.Logic.Workers.Tasker;
using System.Threading.Tasks;

namespace FocLab.Controllers.Mvc
{
    public class ScheduleController : BaseController
    {
        public ScheduleController(ChemistryDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager, IHttpContextAccessor contextAccessor) : base(context, userManager, signInManager)
        {
        }

        private DayTasksWorker TasksWorker => new DayTasksWorker(AmbientContext);

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

        public async Task<IActionResult> Task(string id)
        {
            var model = await TasksWorker.GetDayTaskByIdAsync(id);

            return View(model);
        }
    }
}