using System.Linq;
using System.Threading.Tasks;
using Croco.Core.Contract;
using FocLab.Controllers.Base;
using FocLab.Logic.Models.Users;
using FocLab.Logic.Workers.Users;
using Microsoft.AspNetCore.Mvc;
using Tms.Logic.Models.Tasker;
using Tms.Logic.Workers.Tasker;
using Zoo.GenericUserInterface.Models;
using Zoo.GenericUserInterface.Services;

namespace FocLab.Controllers.Mvc
{
    public class ScheduleController : BaseController
    {
        private UserSearcher UserSearcher { get; }
        private DayTasksWorker TasksWorker { get; }
        private GenericUserInterfaceBag InterfacesBag { get; }

        public ScheduleController(UserSearcher userSearcher,
            DayTasksWorker dayTasksWorker, 
            GenericUserInterfaceBag interfacesBag,
            ICrocoRequestContextAccessor requestContextAccessor)
            : base(requestContextAccessor)
        {
            TasksWorker = dayTasksWorker;
            InterfacesBag = interfacesBag;
            UserSearcher = userSearcher;
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

        public async Task<IActionResult> Task(string id)
        {
            var model = await TasksWorker.GetDayTaskByIdAsync(id);

            //var task = await AmbientContext.RepositoryFactory.Query<ApplicationDayTask>().FirstOrDefaultAsync(x => x.Id == id);

            var users = await UserSearcher.GetUsersAsync(UserSearch.GetAllUsers);

            var userSelectList = users.List.Select(x => new SelectListItem
            {
                //Selected = x.Id == task.AssigneeUserId,
                Text = x.Email,
                Value = x.Id
            });

            var interfaceModel = await InterfacesBag.GetDefaultInterface<CreateOrUpdateDayTask>();
            interfaceModel.Interface.Prefix = "update.";
            ViewData["interfaceModel"] = interfaceModel;
            
            return View(model);
        }
    }
}